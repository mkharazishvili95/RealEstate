using RealEstate.Application.Models;
using RealEstate.Common.Enums.Agency;
using RealEstate.Common.Enums.Apartment;

namespace RealEstate.Application.Feature.User.Apartment.Get
{
    public class GetUserApartmentsResponse : ResponseBaseModel
    {
        public int TotalCount { get; set; }
        public List<GetUserApartmentsItemsResponse> Items { get; set; } = new();
    }
    public class GetUserApartmentsItemsResponse
    {
        public int ApartmentId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public decimal? Price { get; set; }
        public int? CurrencyId { get; set; }
        public int? AgencyId { get; set; }
        public string? AgencyName { get; set; }
        public AgencyType? AgencyType { get; set; }
        public string? AgencyLogoUrl { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public ApartmentDealType ApartmentDealType { get; set; }
        public RealEstate.Common.Enums.Apartment.ApartmentState? ApartmentState { get; set; }
        public BuildingStatus? BuildingStatus { get; set; }
        public int? StreetId { get; set; }
        public int? DistrictId { get; set; }
        public int? SubdistrictId { get; set; }
        public int? CityId { get; set; }
    }
}
