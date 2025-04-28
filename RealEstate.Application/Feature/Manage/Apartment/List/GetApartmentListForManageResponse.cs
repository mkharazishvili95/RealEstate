using RealEstate.Application.Models;
using RealEstate.Common.Enums.Apartment;
using RealEstate.Common.Models;

namespace RealEstate.Application.Feature.Manage.Apartment.List
{
    public class GetApartmentListForManageResponse : ResponseBaseModel
    {
        public List<GetApartmentListForManageItemsResponse> ApartmentListForManage { get; set; } = new();
        public int TotalCount { get; set; }

    }
    public class GetApartmentListForManageItemsResponse
    {
        public int? ApartmentId { get; set; }
        public string? Title { get; set; }
        public ApartmentStatus? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string? UserPin { get; set; }
        public decimal? Price { get; set; }
        public int? CurrencyId { get; set; }
        public int? AgencyId { get; set; }
    }
}
