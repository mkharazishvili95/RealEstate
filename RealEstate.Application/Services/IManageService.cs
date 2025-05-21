using RealEstate.Application.Models.Agency;
using RealEstate.Application.Models.Apartment;
using RealEstate.Application.Models.User;

namespace RealEstate.Application.Services
{
    public interface IManageService
    {
        Task<UserBlockResponseModel> BlockUser(UserBlockRequest request);
        Task<UserUnBlockResponseModel> UnBlockUser(UseUnBlockRequest request);
        Task<TopUpBalanceResponse> TopUpBalance(TopUpBalanceRequest request);
        Task<AgencyApproveResponseModel> ApproveAgency(AgencyApproveRequest request);
        Task<AgencyDeleteResponseModel> DeleteAgency(AgencyDeleteRequest request);
        Task<AgencyRestoreResponseModel> RestoreAgency(AgencyRestoreRequest request);
        Task<BlockApartmentResponse> BlockApartment(BlockApartmentRequest request);
        Task<UnblockApartmentResponse> UnblockApartment(UnblockApartmentRequest request);
    }
}
