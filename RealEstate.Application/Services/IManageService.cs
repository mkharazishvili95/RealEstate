using RealEstate.Application.Feature.Manage.Apartment.Block;
using RealEstate.Application.Feature.Manage.Apartment.Unblock;
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
        Task<BlockApartmentResponse> BlockApartment(BlockApartmentRequest request);
        Task<UnblockApartmentResponse> UnblockApartment(UnblockApartmentRequest request);
    }
}
