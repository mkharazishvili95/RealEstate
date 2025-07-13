using RealEstate.Application.Feature.Profile.Transfer;

namespace RealEstate.Application.Services
{
    public interface IPaymentService
    {
        Task<TransferBalanceResponse> TransferBalanceAsync(TransferBalanceRequest request, CancellationToken cancellationToken = default);
    }
}
