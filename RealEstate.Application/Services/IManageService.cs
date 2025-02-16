using RealEstate.Application.Models.Agency;
using RealEstate.Application.Models.User;

namespace RealEstate.Application.Services
{
    public interface IManageService
    {
        Task<UserBlockResponseModel> BlockUser(string id);
        Task<UserUnBlockResponseModel> UnBlockUser(string id);
        Task<TopUpBalanceResponse> TopUpBalance(string id, decimal balance);
        Task<AgencyDeleteResponseModel> DeleteAgency(int agencyId, string? deleteReason);
        Task<AgencyRestoreResponseModel> RestoreAgency(int agencyId);
    }
}
