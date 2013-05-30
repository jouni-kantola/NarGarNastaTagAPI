namespace NarGarNastaTag.API.Contract
{
    public interface ITrainRoute
    {
        string Id { get; set; }
        int RouteNo { get; set; }
        string TrainOperator { get; set; }
        RouteStop FromStation { get; set; }
        RouteStop ToStation { get; set; }
        string StartDate { get; set; }
    }
}