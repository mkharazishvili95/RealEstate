namespace RealEstate.Application.Feature.User.Favorite.RemoveFromFavorites
{
    public class RemoveFromFavoritesRequest : IRequest<RemoveFromFavoritesResponse>
    {
        public string UserId { get; set; }
        public int ApartmentId { get; set; }
    }
}
