using Nancy;
using NarGarNastaTag.API.Models;

namespace NarGarNastaTag.API.Modules
{
    public class StaticRouteModule : NancyModule
    {
        public StaticRouteModule(ISettingsProvider settingsProvider)
        {
            Get["/"] = _ => Response.AsText(settingsProvider.SimpleContactInformation).WithStatusCode(HttpStatusCode.OK);
        }
    }
}