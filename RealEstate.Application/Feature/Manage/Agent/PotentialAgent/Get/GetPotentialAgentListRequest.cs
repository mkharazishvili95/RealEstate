using RealEstate.Common.Models;

namespace RealEstate.Application.Feature.Manage.Agent.PotentialAgent.Get
{
    public class GetPotentialAgentListRequest : IRequest<GetPotentialAgentListResponse>
    {
        public int? Status { get; set; }
        public Pagination? Pagination { get; set; }
    }
}
