using RealEstate.Application.Models;
using RealEstate.Common.Enums.Apartment;

namespace RealEstate.Application.Feature.Profile.Apartments
{
    public class MyApartmentsResponse : ResponseBaseModel
    {
        public List<MyApartmentsItemsResponse> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
    public class MyApartmentsItemsResponse
    {
        public int ApartmentId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ApartmentStatus? Status { get; set; }
        public string? BlockReason { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public decimal? Price { get; set; }
        public int? CurrencyId { get; set; }
        public int? AgencyId { get; set; }
        public string? AgencyName { get; set; }
    }
}
