using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Models.Agency;
using RealEstate.Application.Models.Apartment;
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
            if (request == null)
                return new UserBlockResponseModel { StatusCode = 400, Success = false, UserMessage = "request should not be empty." };

            var userId = request.UserId;
            var user = await _identityService.GetUserById(userId);
            if (user == null)
                return new UserBlockResponseModel { StatusCode = 404, Success = false, UserMessage = "User not found." };

            if (user.IsBlocked)
                return new UserBlockResponseModel { StatusCode = 400, Success = false, UserMessage = "User is already blocked" };

            user.IsBlocked = true;
            user.BlockReason = request.BlockReason ?? null;
            user.BlockDate = DateTime.UtcNow;

            await _db.SaveChangesAsync(CancellationToken.None);
            return new UserBlockResponseModel { Success = true, StatusCode = 200 };
        }
        public async Task<UserUnBlockResponseModel> UnBlockUser(UseUnBlockRequest request)
        {
            if (request == null)
                return new UserUnBlockResponseModel { StatusCode = 400, Success = false, UserMessage = "request should not be empty." };

            var userId = request.UserId;
            var user = await _identityService.GetUserById(userId);
            if (user == null)
                return new UserUnBlockResponseModel { StatusCode = 404, Success = false, UserMessage = "User not found." };

            if (!user.IsBlocked)
                return new UserUnBlockResponseModel { StatusCode = 400, Success = false, UserMessage = "User is not blocked." };

            user.IsBlocked = false;
            user.BlockDate = null;
            user.BlockReason = null;

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

        public async Task<AgencyDeleteResponseModel> DeleteAgency(AgencyDeleteRequest request)
        {
            if (request.AgencyId <= 0)
                return new AgencyDeleteResponseModel { StatusCode = 400, Success = false, UserMessage = "request should not be empty." };

            var agency = await _agencyService.GetAgencyById(request.AgencyId);
            if (agency == null) return new AgencyDeleteResponseModel { StatusCode = 404, Success = false, UserMessage = $"Agency with id: {agency} not found." };
            if (agency.IsDeleted) return new AgencyDeleteResponseModel { StatusCode = 404, Success = false, UserMessage = $"Agency is already deleted." };

            agency.IsDeleted = true;
            agency.DeleteDate = DateTime.Now;
            agency.IsApproved = false;
            agency.DeleteReason = request.DeleteReason ?? null;
            await _db.SaveChangesAsync(CancellationToken.None);
            return new AgencyDeleteResponseModel { StatusCode = 200, Success = true };
        }

        public async Task<AgencyRestoreResponseModel> RestoreAgency(AgencyRestoreRequest request)
        {
            if (request == null)
                return new AgencyRestoreResponseModel { StatusCode = 400, Success = false, UserMessage = "request should not be empty." };

            var agency = await _agencyService.GetAgencyById(request.AgencyId);
            if (agency == null) return new AgencyRestoreResponseModel { StatusCode = 404, Success = false, UserMessage = $"Agency with id: {agency} not found." };
            if (!agency.IsDeleted) return new AgencyRestoreResponseModel { StatusCode = 404, Success = false, UserMessage = $"Agency is not deleted." };

            agency.IsDeleted = false;
            agency.DeleteDate = null;
            agency.IsApproved = false;
            await _db.SaveChangesAsync(CancellationToken.None);
            return new AgencyRestoreResponseModel { StatusCode = 200, Success = true };
        }

        public async Task<BlockApartmentResponse> BlockApartment(BlockApartmentRequest request)
        {
            if (request.ApartmentId == null || request.ApartmentId <= 0)
                return new BlockApartmentResponse { Success = false, StatusCode = 400, UserMessage = "request should not be empty." };

            var apartment = await _db.Apartments
                .FirstOrDefaultAsync(x => x.ApartmentId == request.ApartmentId && x.Status != Common.Enums.Apartment.ApartmentStatus.Blocked);

            if (apartment == null)
            {
                return new BlockApartmentResponse { Success = false, StatusCode = 404, UserMessage = "apartment not found or its already blocked." };
            }
            else
            {
                apartment.Status = Common.Enums.Apartment.ApartmentStatus.Blocked;
                apartment.BlockReason = request.BlockReason ?? null;
                await _db.SaveChangesAsync();
                return new BlockApartmentResponse { Success = true, StatusCode = 200, UserMessage = "Successfully blocked." };
            }
        }

        public async Task<UnblockApartmentResponse> UnblockApartment(UnblockApartmentRequest request)
        {
            if (request == null)
                return new UnblockApartmentResponse { Success = false, StatusCode = 400, UserMessage = "request should not be empty." };

            var apartment = await _db.Apartments
                .FirstOrDefaultAsync(x => x.ApartmentId == request.ApartmentId && x.Status == Common.Enums.Apartment.ApartmentStatus.Blocked);

            if (apartment == null)
            {
                return new UnblockApartmentResponse { Success = false, StatusCode = 404, UserMessage = "Apartment not found or its not blocked." };
            }
            else
            {
                apartment.Status = Common.Enums.Apartment.ApartmentStatus.Active;
                apartment.BlockReason = null;
                await _db.SaveChangesAsync();
                return new UnblockApartmentResponse { Success = true, StatusCode = 200, UserMessage = "Successfully unblocked." };
            }
        }
    }
}
