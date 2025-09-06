namespace RealEstate.Application.Feature.Manage.Agent.ChangeStatus.IndividualIntoAgent
{
    public class ChangeIndividualToAgentRequest : IRequest<ChangeIndividualToAgentResponse>
    {
        public string PIN { get; set; }
    }
}
