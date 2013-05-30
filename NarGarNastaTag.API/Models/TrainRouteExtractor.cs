using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using NarGarNastaTag.API.Contract;
using NarGarNastaTag.API.Extensions;

namespace NarGarNastaTag.API.Models
{
    class TrainRouteExtractor : IHtmlExtractor<TrainRoute>
    {
        private readonly string _fromId;
        private readonly string _toId;
        private readonly DateTime _routeDate;

        public TrainRouteExtractor(string fromId, string toId, DateTime routeDate)
        {
            _fromId = Uri.UnescapeDataString(fromId);
            _toId = Uri.UnescapeDataString(toId);
            _routeDate = routeDate;
        }

        public IEnumerable<TrainRoute> ExtractData(HtmlDocument htmlDocument)
        {
            var allStations = from tr in htmlDocument.DocumentNode.SelectNodes("//tr[@class]")
                              where Regex.IsMatch(tr.OuterHtml, "FavouriteDataGridItem")
                                && (Regex.IsMatch(Uri.UnescapeDataString(tr.OuterHtml), string.Format("{0}&", _fromId), RegexOptions.IgnoreCase)
                                   || Regex.IsMatch(Uri.UnescapeDataString(tr.OuterHtml), string.Format("{0}&", _toId), RegexOptions.IgnoreCase))
                              select new
                              {
                                  StationHtml = tr.ChildNodes[1].InnerHtml,
                                  ArrivalHtml = tr.ChildNodes[2].InnerHtml,
                                  DepartureHtml = tr.ChildNodes[3].InnerHtml,
                                  TrackHtml = tr.ChildNodes[4].InnerHtml,
                                  CommentHtml = tr.ChildNodes[5].InnerHtml
                              };

            var trainRoute = new TrainRoute {Id = string.Concat(_fromId, "-", _toId)};
            foreach (var station in allStations)
            {
                if (RouteIsNotCorrectDirection(station.StationHtml, trainRoute.FromStation == null)) return Enumerable.Empty<TrainRoute>();
                var routeStop = new RouteStop
                {
                    StationName = Regex.Match(station.StationHtml, @"TrafficSituationCity.*\""\>(?<StationName>[\w\s]+)\<").Groups["StationName"].Value.Trim(),
                    ScheduledArrival = Regex.Match(station.ArrivalHtml, @">(?<ScheduledArrival>.{5,6})\<\/div>").Groups["ScheduledArrival"].Value.DecodeNonBreakingSpace().Trim(),
                    UpdatedArrival = Regex.Match(station.ArrivalHtml, @"Beräknas.*>(?<UpdatedArrival>.{5,6})\<\/").Groups["UpdatedArrival"].Value.DecodeNonBreakingSpace().Trim(),
                    ScheduledDeparture = Regex.Match(station.DepartureHtml, @">(?<ScheduledDeparture>.{5,6})\<\/div>").Groups["ScheduledDeparture"].Value.DecodeNonBreakingSpace().Trim(),
                    UpdatedDeparture = Regex.Match(station.ArrivalHtml, @"Beräknas.*>(?<UpdatedDeparture>.{5,6})\<\/").Groups["UpdatedDeparture"].Value.DecodeNonBreakingSpace().Trim(),
                    IsCancelled = Regex.IsMatch(station.DepartureHtml, "inställt", RegexOptions.IgnoreCase),
                    HasDeparted = Regex.IsMatch(station.DepartureHtml, "avgick", RegexOptions.IgnoreCase),
                    Track = Regex.Match(station.TrackHtml, @">(?<Track>\w{1,3})\<\/").Groups["Track"].Value.DecodeNonBreakingSpace().Trim().ToUpper(),
                    IsDestination = trainRoute.FromStation != null,
                    TrackChanged = Regex.IsMatch(station.CommentHtml, @"ändr[\w\s]*spår|spår[\w\s]*ändr", RegexOptions.IgnoreCase),
                };
                if (trainRoute.FromStation == null)
                    trainRoute.FromStation = routeStop;
                else
                    trainRoute.ToStation = routeStop;
            }

            if (trainRoute.FromStation == null || trainRoute.ToStation == null || trainRoute.FromStation.HasDeparted) return Enumerable.Empty<TrainRoute>();

            trainRoute.StartDate = _routeDate.ToString("yyyyMMdd");

            var routeNo = htmlDocument.DocumentNode.SelectNodes("//div[@id=\"TrafficInfoTrainHeader\"]")
                                      .Select(div => Regex.Match(div.InnerHtml, @"tåg\s*(?<TrainNo>\d+)").Groups["TrainNo"].Value);
            if(routeNo.Any())
                trainRoute.RouteNo = int.Parse(routeNo.First());

            var trainOperatorLinks = htmlDocument.DocumentNode.SelectNodes("//div[@id=\"Trafficsituationtraincomponent_JFLankPanel\"]");
            if (trainOperatorLinks != null)
            {
                trainRoute.TrainOperator =
                    Regex.Match(trainOperatorLinks.First().InnerHtml, @"Järnvägsföretag.*\>(?<TrainOperator>.+)\<").Success
                        ? Regex.Match(trainOperatorLinks.First().InnerHtml, @"Järnvägsföretag.*\>(?<TrainOperator>.+)\<").Groups["TrainOperator"].Value
                        : string.Empty;
            }
            return new List<TrainRoute> { trainRoute };
        }

        private bool RouteIsNotCorrectDirection(string stationHtml, bool isFirstRouteStop)
        {
            return isFirstRouteStop && Regex.IsMatch(Uri.UnescapeDataString(stationHtml), string.Format("{0}&", _toId), RegexOptions.IgnoreCase);
        }
    }
}