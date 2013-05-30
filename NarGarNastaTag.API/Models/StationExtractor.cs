using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using NarGarNastaTag.API.Contract;
using NarGarNastaTag.API.Extensions;

namespace NarGarNastaTag.API.Models
{
    class StationExtractor : IHtmlExtractor<Station>
    {
        public IEnumerable<Station> ExtractData(HtmlDocument htmlDocument)
        {
            var routeNumbersAndDates = from option in htmlDocument.DocumentNode.SelectNodes("//option[@value]")
                                       select new {option};

            return routeNumbersAndDates.Select(station => new Station
                {
                    Name = WebUtility.HtmlDecode(station.option.NextSibling.InnerText.DecodeNonBreakingSpace().Trim()),
                    Id = WebUtility.HtmlDecode(station.option.Attributes["value"].Value.ToUpper().Trim())
                }).Where(station => !string.IsNullOrEmpty(station.Id));
        }
    }
}