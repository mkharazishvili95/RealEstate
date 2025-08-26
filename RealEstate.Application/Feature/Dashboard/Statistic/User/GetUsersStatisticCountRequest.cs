namespace RealEstate.Application.Feature.Dashboard.Statistic.User
{
    public class GetUsersStatisticCountRequest : IRequest<GetUsersStatisticCountResponse>
    {
        public DateTime DateFrom { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DateTo { get; set; } = DateTime.Now;
    }
}