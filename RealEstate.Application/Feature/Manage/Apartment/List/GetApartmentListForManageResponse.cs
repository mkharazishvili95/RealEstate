using RealEstate.Application.Models;
using RealEstate.Common.Enums.Apartment;

namespace RealEstate.Application.Feature.Manage.Apartment.List
{
    public class GetApartmentListForManageResponse : ResponseBaseModel
    {
        public List<GetApartmentListForManageItemsResponse> ApartmentListForManage { get; set; } = new();
        public int? PageSize { get; set; } = 20;
        public int? Page { get; set; } = 1;
        public int? Skip => (Page!.Value - 1) * PageSize!.Value;

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
        public decimal? UnitPrice { get; set; }
        public int? CurrencyId { get; set; }
        public int? AgencyId { get; set; }
    }
}
