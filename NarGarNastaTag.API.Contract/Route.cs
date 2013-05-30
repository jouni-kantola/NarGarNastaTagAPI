namespace NarGarNastaTag.API.Contract
{
    public class Route : IRoute
    {
        public string Name { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public string Date { get; set; }
        public string RouteNo { get; set; }
        public string Url { get; set; }
    }
}