namespace NarGarNastaTag.API.Contract
{
    public class TrainRoute : ITrainRoute
    {
        public string Id { get; set; }
        public int RouteNo { get; set; }
        public string TrainOperator { get; set; }
        public RouteStop FromStation { get; set; }
        public RouteStop ToStation { get; set; }
        public string StartDate { get; set; }
    }
}