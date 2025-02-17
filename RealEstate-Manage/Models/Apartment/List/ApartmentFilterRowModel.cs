using RealEstate.Common.Enums.Apartment;

namespace RealEstate_Manage.Models.Apartment.List
{
    public class ApartmentFilterRowModel
    {
        public int? ApartmentId { get; set; }
        public string? Title { get; set; }
        public ApartmentStatus? Status { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public DateTime? UpdateDateFrom { get; set; }
        public DateTime? UpdateDateTo { get; set; }
        public DateTime? DeleteDateFrom { get; set; }
        public DateTime? DeleteDateTo { get; set; }
        public string? UserId { get; set; }
        public string? UserPin { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public decimal? UnitPriceFrom { get; set; }
        public decimal? UnitPriceTo { get; set; }
        public int? CurrencyId { get; set; }
        public int? AgencyId { get; set; }
        public int? PageSize { get; set; } = 20;
        public int? Page { get; set; } = 1;
    }
}
