namespace NarGarNastaTag.API.Contract
{
    public class FavouriteRoute : IFavouriteRoute
    {
        public string FromId { get; set; }
        public string ToId { get; set; }
    }
}