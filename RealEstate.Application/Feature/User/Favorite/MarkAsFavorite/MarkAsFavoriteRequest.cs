namespace RealEstate.Application.Feature.User.Favorite.MarkAsFavorite
{
    public class MarkAsFavoriteRequest : IRequest<MarkAsFavoriteResponse>   
    {
        public string UserId { get; set; }
        public int ApartmentId { get; set; }
        public string? Comment { get; set; }
    }
}
