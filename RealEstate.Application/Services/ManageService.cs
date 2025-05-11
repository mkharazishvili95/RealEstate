using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Models.Agency;
using RealEstate.Application.Models.User;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Services
{
    public class ManageService : IManageService
    {
        readonly ApplicationDbContext _db;
        readonly IIdentityService _identityService;
        readonly IAgencyService _agencyService;

        public ManageService(ApplicationDbContext db, IIdentityService identityService, IAgencyService agencyService)
        {
            _db = db;
            _identityService = identityService;
            _agencyService = agencyService;
        }

        public async Task<UserBlockResponseModel> BlockUser(UserBlockRequest request)
        {
            if(request == null)
                return new UserBlockResponseModel { StatusCode = 400, Success = false, UserMessage = "request should not be empty." };

            var userId = request.UserId;
            var user = await _identityService.GetUserById(userId);
            if (user == null)
                return new UserBlockResponseModel { StatusCode = 404, Success = false, UserMessage = "User not found." };

            if (user.IsBlocked)
                return new UserBlockResponseModel { StatusCode = 400, Success = false, UserMessage = "User is already blocked" };

            user.IsBlocked = true;
            user.BlockDate = DateTime.UtcNow;

            await _db.SaveChangesAsync(CancellationToken.None);
            return new UserBlockResponseModel { Success = true, StatusCode = 200 };
        }
        public async Task<UserUnBlockResponseModel> UnBlockUser(UseUnBlockRequest request)
        {
            if(request == null)
                return new UserUnBlockResponseModel { StatusCode = 400, Success = false, UserMessage = "request should not be empty." };

            var userId = request.UserId;
            var user = await _identityService.GetUserById(userId);
            if (user == null)
                return new UserUnBlockResponseModel { StatusCode = 404, Success = false, UserMessage = "User not found." };

            if (!user.IsBlocked)
                return new UserUnBlockResponseModel { StatusCode = 400, Success = false, UserMessage = "User is not bloocked." };

            user.IsBlocked = false;
            user.BlockDate = null;

            await _db.SaveChangesAsync(CancellationToken.None);
            return new UserUnBlockResponseModel { Success = true, StatusCode = 200 };
        }

        public async Task<TopUpBalanceResponse> TopUpBalance(TopUpBalanceRequest request)
        {
            if (request == null)
                return new TopUpBalanceResponse { StatusCode = 400, Success = false, UserMessage = "request should not be empty" };

            var userId = request.UserId;    

            var user = await _identityService.GetUserById(userId);

            if (request.Balance <= 0)
                return new TopUpBalanceResponse { StatusCode = 404, Success = false, UserMessage = "Enter correct amount." };

            if (user == null)
            {
                return new TopUpBalanceResponse { StatusCode = 404, Success = false, UserMessage = "User not found." };
            }
            else
            {
                user.Balance += request.Balance;
                await _db.SaveChangesAsync(CancellationToken.None);
                return new TopUpBalanceResponse { StatusCode = 200, Success = true, UserMessage = "Success." };
            }
        }

        public async Task<AgencyDeleteResponseModel> DeleteAgency(int agencyId, string? deleteReason)
        {
            var agency = await _agencyService.GetAgencyById(agencyId);
            if (agency == null) return new AgencyDeleteResponseModel { StatusCode = 404, Success = false, UserMessage = $"Agency with id: {agency} not found." };
            if (agency.IsDeleted) return new AgencyDeleteResponseModel { StatusCode = 404, Success = false, UserMessage = $"Agency is already deleted." };

            agency.IsDeleted = true;
            agency.DeleteDate = DateTime.Now;
            agency.IsApproved = false;
            agency.DeleteReason = deleteReason != null ? deleteReason : null;
            await _db.SaveChangesAsync(CancellationToken.None);
            return new AgencyDeleteResponseModel { StatusCode = 200, Success = true };
        }

        public async Task<AgencyRestoreResponseModel> RestoreAgency(int agencyId)
        {
            var agency = await _agencyService.GetAgencyById(agencyId);
            if (agency == null) return new AgencyRestoreResponseModel { StatusCode = 404, Success = false, UserMessage = $"Agency with id: {agency} not found." };
            if (!agency.IsDeleted) return new AgencyRestoreResponseModel { StatusCode = 404, Success = false, UserMessage = $"Agency is not deleted." };

            agency.IsDeleted = false;
            agency.DeleteDate = null;
            agency.IsApproved = true;
            await _db.SaveChangesAsync(CancellationToken.None);
            return new AgencyRestoreResponseModel { StatusCode = 200, Success = true };
        }
    }
}
