using RealEstate.Common.Enums.Apartment;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.Feature.Apartment.Create
{
    public class CreateApartmentRequest : IRequest<CreateApartmentResponse>
    {
        public string? UserId { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? CurrencyId { get; set; }
        public int? AgencyId { get; set; }
        [Required]
        public ApartmentType ApartmentType { get; set; }
        [Required]
        public ApartmentDealType ApartmentDealType { get; set; }
        public RealEstate.Common.Enums.Apartment.ApartmentState? ApartmentState { get; set; }
        public BuildingStatus? BuildingStatus { get; set; }
        public int? StreetId { get; set; }
        public int? DistrictId { get; set; }
        public int? SubdistrictId { get; set; }
        public int? CityId { get; set; }
    }
}
