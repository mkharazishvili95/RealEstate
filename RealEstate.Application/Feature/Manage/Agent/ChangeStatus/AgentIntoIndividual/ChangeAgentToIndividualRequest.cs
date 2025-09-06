namespace RealEstate.Application.Feature.Manage.Agent.ChangeStatus.AgentIntoIndividual
{
    public class ChangeAgentToIndividualRequest : IRequest<ChangeAgentToIndividualResponse>
    {
        public string PIN { get; set; }
    }
}
