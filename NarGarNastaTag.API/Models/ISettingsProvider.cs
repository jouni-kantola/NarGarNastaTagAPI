using System;

namespace NarGarNastaTag.API.Models
{
    public interface ISettingsProvider
    {
        string GetAllStationsUrl();
        string GetRouteUrl(DateTime date, int trainNo);
        string GetStationRoutesUrl(string fromStationId);
        string ApiKey { get; }
        string AllowOriginPrimary { get; }
        string AllowOriginSecondary { get; }
        string SimpleContactInformation { get; }
    }
}