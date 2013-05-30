using System.Linq;
using HtmlAgilityPack;

namespace NarGarNastaTag.API.Extensions
{
    public static class HtmlAgilityPackExtensions
    {
        public static bool HasHeaderWhereValue(this HtmlAttributeCollection collection, string attributeValue)
        {
            return collection.Any(c => c.IsHeaderWithValue(attributeValue));
        }

        public static bool IsHeaderWithValue(this HtmlAttribute htmlAttribute, string attributeValue)
        {
            return htmlAttribute.Name.ToLower() == "headers" && htmlAttribute.Value.ToLower() == attributeValue.ToLower();
        }
    }
}