using System.Text.RegularExpressions;
using Nancy;

namespace NarGarNastaTag.API.Extensions
{
    public static class NancyExtensions
    {
        public static bool IsCrawler(this RequestHeaders requestHeaders)
        {
            return Regex.IsMatch(requestHeaders.UserAgent,
                                 @"bot|crawler|baiduspider|80legs|ia_archiver|voyager|curl|wget|yahoo! slurp|mediapartners-google",
                                 RegexOptions.IgnoreCase);
        }

        public static void EnableCors(this NancyModule module, string origin)
        {
            module.After.AddItemToEndOfPipeline(x => x.Response.WithHeader("Access-Control-Allow-Origin", origin));
        }
    }
}