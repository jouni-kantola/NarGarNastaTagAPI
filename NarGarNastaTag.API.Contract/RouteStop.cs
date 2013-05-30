namespace NarGarNastaTag.API.Contract
{
    public class RouteStop : IRouteStop
    {
        public string StationName { get; set; }
        public bool IsDestination { get; set; }
        public string ScheduledDeparture { get; set; }
        public string ScheduledArrival { get; set; }
        public string UpdatedDeparture { get; set; }
        public string UpdatedArrival { get; set; }
        public bool HasDeparted { get; set; }
        public string Track { get; set; }
        public bool TrackChanged { get; set; }
        public bool IsCancelled { get; set; }
    }
}