using RealEstate.Application.Models.Agency;
using RealEstate.Application.Models.User;

namespace RealEstate.Application.Services
{
    public interface IManageService
    {
        Task<UserBlockResponseModel> BlockUser(UserBlockRequest request);
        Task<UserUnBlockResponseModel> UnBlockUser(UseUnBlockRequest request);
        Task<TopUpBalanceResponse> TopUpBalance(TopUpBalanceRequest request);
        Task<AgencyDeleteResponseModel> DeleteAgency(int agencyId, string? deleteReason);
        Task<AgencyRestoreResponseModel> RestoreAgency(int agencyId);
    }
}
