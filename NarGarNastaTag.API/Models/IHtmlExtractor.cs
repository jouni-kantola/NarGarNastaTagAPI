using System.Collections.Generic;
using HtmlAgilityPack;

namespace NarGarNastaTag.API.Models
{
    public interface IHtmlExtractor<T> where T : new()
    {
        IEnumerable<T> ExtractData(HtmlDocument htmlDocument);
    }
}