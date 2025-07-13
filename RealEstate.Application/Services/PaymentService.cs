using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Feature.Profile.Transfer;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IIdentityService _identityService;

        public PaymentService(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }
        public async Task<TransferBalanceResponse> TransferBalanceAsync(TransferBalanceRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.SenderUserId) ||
                string.IsNullOrWhiteSpace(request.ReceiverPIN) ||
                request.Amount <= 0)
                return new TransferBalanceResponse{ Success = false, StatusCode = 400, UserMessage = "Invalid transfer request." };

            var sender = await _identityService.GetUserById(request.SenderUserId);
            if (sender == null)
                return new TransferBalanceResponse { Success = false, StatusCode = 404, UserMessage = "Sender not found." };

            if (sender.IsBlocked)
                return new TransferBalanceResponse { Success = false, StatusCode = 400, UserMessage = "Sender is blocked." };

            var receiver = await _db.Users.FirstOrDefaultAsync(u => u.PIN == request.ReceiverPIN, cancellationToken);
            if (receiver == null)
                return new TransferBalanceResponse { Success = false, StatusCode = 404, UserMessage = "Receiver not found." };

            if (receiver.UserId == sender.UserId)
                return new TransferBalanceResponse { Success = false, StatusCode = 400, UserMessage = "Cannot transfer to yourself." };

            if (sender.Balance < request.Amount)
                return new TransferBalanceResponse { Success = false, StatusCode = 400, UserMessage = "Insufficient balance." };

            using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                sender.Balance -= request.Amount;
                receiver.Balance += request.Amount;

                _db.Users.Update(sender);
                _db.Users.Update(receiver);

                await _db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new TransferBalanceResponse { Success = true, StatusCode = 200, UserMessage = $"Transferred {request.Amount:C} successfully." };

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new TransferBalanceResponse { Success = false, StatusCode = 500, UserMessage = $"An error occurred during transfer: {ex.Message}" };
            }
        }
    }
}
