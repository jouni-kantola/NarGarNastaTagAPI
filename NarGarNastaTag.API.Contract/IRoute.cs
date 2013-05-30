namespace NarGarNastaTag.API.Contract
{
    public interface IRoute
    {
        string Name { get; set; }
        string FromId { get; set; }
        string ToId { get; set; }
        string Date { get; set; }
        string RouteNo { get; set; }
        string Url { get; set; }
    }
}