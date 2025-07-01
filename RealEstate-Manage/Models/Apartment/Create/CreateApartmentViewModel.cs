using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Common.Enums.Apartment;
using System.ComponentModel.DataAnnotations;

namespace RealEstate_Manage.Models.Apartment.Create
{
    public class CreateApartmentViewModel
    {
        public string? UserId { get; set; }
        [Required(ErrorMessage = "სათაური აუცილებელია")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? CurrencyId { get; set; }
        public int? AgencyId { get; set; }
        [Required(ErrorMessage = "ბინის ტიპი აუცილებელია")]
        public ApartmentType ApartmentType { get; set; }
        [Required(ErrorMessage = "გარიგების ტიპი აუცილებელია")]
        public ApartmentDealType ApartmentDealType { get; set; }
        public RealEstate.Common.Enums.Apartment.ApartmentState? ApartmentState { get; set; }
        public BuildingStatus? BuildingStatus { get; set; }
        public int? StreetId { get; set; }
        public int? DistrictId { get; set; }
        public int? SubdistrictId { get; set; }
        public int? CityId { get; set; }

        public int? SelectedAgencyId { get; set; }
        public List<SelectListItem>? Agencies { get; set; }
    }
}
