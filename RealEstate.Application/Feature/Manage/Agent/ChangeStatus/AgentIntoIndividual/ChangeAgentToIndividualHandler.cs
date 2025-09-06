using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Agent.ChangeStatus.AgentIntoIndividual
{
    public class ChangeAgentToIndividualHandler : IRequestHandler<ChangeAgentToIndividualRequest, ChangeAgentToIndividualResponse>
    {
        readonly ApplicationDbContext _db;
        public ChangeAgentToIndividualHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ChangeAgentToIndividualResponse> Handle(ChangeAgentToIndividualRequest request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.PIN))
                return new ChangeAgentToIndividualResponse {  StatusCode = 400, Success = false, UserMessage = "PIN is required" };

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.PIN == request.PIN, cancellationToken);

            if (user == null)
                return new ChangeAgentToIndividualResponse { StatusCode = 404, Success = false, UserMessage = "User not found" };

            if(user.Type == Common.Enums.User.UserType.Individual)
                return new ChangeAgentToIndividualResponse { StatusCode = 400, Success = false, UserMessage = "User is already an Individeual" };

            user.Type = Common.Enums.User.UserType.Individual;
            await _db.SaveChangesAsync(cancellationToken);
            return new ChangeAgentToIndividualResponse { StatusCode = 200, Success = true, UserMessage = "User type changed to Individual successfully" };
        }
    }
}
