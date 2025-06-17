namespace RealEstate.Core.PaidService
{
    public class Tariff
    {
        public int Id { get; set; }
        public RealEstate.Common.Enums.PaidService.PaidService? PaidService { get; set; }
        public decimal Price { get; set; }
    }
}
