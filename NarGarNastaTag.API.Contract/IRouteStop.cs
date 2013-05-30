namespace NarGarNastaTag.API.Contract
{
    public interface IRouteStop
    {
        string StationName { get; set; }
        bool IsDestination { get; set; }
        string ScheduledDeparture { get; set; }
        string ScheduledArrival { get; set; }
        string UpdatedDeparture { get; set; }
        string UpdatedArrival { get; set; }
        bool HasDeparted { get; set; }
        string Track { get; set; }
        bool TrackChanged { get; set; }
        bool IsCancelled { get; set; }
    }
}