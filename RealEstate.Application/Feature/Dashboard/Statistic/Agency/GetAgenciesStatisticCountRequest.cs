namespace RealEstate.Application.Feature.Dashboard.Statistic.Agency
{
    public class GetAgenciesStatisticCountRequest : IRequest<GetAgenciesStatisticCountResponse>
    {
        public DateTime DateFrom { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DateTo { get; set; } = DateTime.Now;
    }
}
