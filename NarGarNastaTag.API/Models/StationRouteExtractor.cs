using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using NarGarNastaTag.API.Contract;

namespace NarGarNastaTag.API.Models
{
    class StationRouteExtractor : IHtmlExtractor<Route>
    {
        private readonly string _fromStationId;

        public StationRouteExtractor(string fromStationId)
        {
            _fromStationId = fromStationId;
        }

        public IEnumerable<Route> ExtractData(HtmlDocument htmlDocument)
        {
            var routeNumbersAndDates = from a in htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                                       where Regex.IsMatch(a.OuterHtml, "TrafficSituationTrain")
                                       select new { DateAndRoute = Regex.Match(a.OuterHtml, @"\w+,\w+").Value, Url = Regex.Match(a.OuterHtml, "href=\\\"(?<Url>\\S+)\\\"").Groups["Url"].Value };

            var routes = routeNumbersAndDates.Select(link => new Route
                {
                    Date = link.DateAndRoute.Split(',')[0],
                    FromId = WebUtility.UrlDecode(_fromStationId),
                    RouteNo = link.DateAndRoute.Split(',')[1],
                    Url = link.Url
                });
            return routes;
        }

    }
}