namespace RealEstate_Manage.Models.Profile
{
    public class MyApartmentViewModel
    {
        public int ApartmentId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
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
