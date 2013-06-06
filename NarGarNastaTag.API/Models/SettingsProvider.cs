using System;
using System.Configuration;

namespace NarGarNastaTag.API.Models
{
    class SettingsProvider : ISettingsProvider
    {
        private readonly IServerRandomizer _serverRandomizer;

        public SettingsProvider(IServerRandomizer serverRandomizer)
        {
            _serverRandomizer = serverRandomizer;
        }

        public string GetAllStationsUrl()
        {
            return string.Format("http://www{0}.trafikverket.se/Trafikinformation/WebPage/StartPage.aspx", _serverRandomizer.GetServerNumber());
        }

        public string GetRouteUrl(DateTime date, int trainNo)
        {
            return string.Format("http://www{0}.trafikverket.se/Trafikinformation/WebPage/TrafficSituationTrain.aspx?JF=14&train={1},{2}", _serverRandomizer.GetServerNumber(), date.ToString("yyyyMMdd"), trainNo);
        }

        public string GetStationRoutesUrl(string fromStationId)
        {
            return string.Format("http://www{0}.trafikverket.se/Trafikinformation/WebPage/TrafficSituationCity.aspx?JF=14&station={1}&arrivals=0&nostat=1", _serverRandomizer.GetServerNumber(), fromStationId);
        }

        public string ApiKey
        {
            get { return ConfigurationManager.AppSettings["API_KEY"]; }
        }
        
        public string AllowOriginPrimary
        {
            get { return ConfigurationManager.AppSettings["ALLOW_ORIGIN_PRIMARY"]; }
        }

        public string AllowOriginSecondary
        {
            get { return ConfigurationManager.AppSettings["ALLOW_ORIGIN_SECONDARY"]; }
        }

        public string SimpleContactInformation
        {
            get { return ConfigurationManager.AppSettings["SIMPLE_CONTACT_INFORMATION"]; }
        }
    }
}