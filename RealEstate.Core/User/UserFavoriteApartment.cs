namespace RealEstate.Core.User
{
    public class UserFavoriteApartment
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public Core.Apartment.Apartment Apartment { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string? Comment { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
