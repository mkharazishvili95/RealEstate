using RealEstate.Common.Enums.Apartment;

namespace RealEstate.Infrastructure.Queries.Models.Apartment
{
    public class GetApartmentDetailsModel
    {
        public int ApartmentId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ApartmentStatus? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string? UserId { get; set; }
        public decimal? Price { get; set; }
        public int? CurrencyId { get; set; }
        public Core.Currency.Currency? Currency { get; set; }
        public int? AgencyId { get; set; }
    }
}
