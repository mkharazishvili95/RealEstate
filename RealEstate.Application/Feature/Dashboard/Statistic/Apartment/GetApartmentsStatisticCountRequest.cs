namespace RealEstate.Application.Feature.Dashboard.Statistic.Apartment
{
    public class GetApartmentsStatisticCountRequest : IRequest<GetApartmentsStatisticCountResponse>
    {
        public DateTime DateFrom { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DateTo { get; set; } = DateTime.Now;
    }
}
