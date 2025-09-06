using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Agent.ChangeStatus.IndividualIntoAgent
{
    public class ChangeIndividualToAgentHandler : IRequestHandler<ChangeIndividualToAgentRequest, ChangeIndividualToAgentResponse>
    {
        readonly ApplicationDbContext _db;
        public ChangeIndividualToAgentHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ChangeIndividualToAgentResponse> Handle(ChangeIndividualToAgentRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.PIN))
                return new ChangeIndividualToAgentResponse { StatusCode = 400, Success = false, UserMessage = "PIN is required" };

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.PIN == request.PIN, cancellationToken);

            if (user == null)
                return new ChangeIndividualToAgentResponse { StatusCode = 404, Success = false, UserMessage = "User not found" };

            if (user.Type == Common.Enums.User.UserType.Agent)
                return new ChangeIndividualToAgentResponse { StatusCode = 400, Success = false, UserMessage = "User is already an Agent" };

            user.Type = Common.Enums.User.UserType.Agent;
            await _db.SaveChangesAsync(cancellationToken);
            return new ChangeIndividualToAgentResponse { StatusCode = 200, Success = true, UserMessage = "User type changed to Agent successfully" };
        }
    }
}
