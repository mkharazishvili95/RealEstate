using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Apartment.Unblock
{
    public class UnblockApartmentHandler : IRequestHandler<UnblockApartmentRequest, UnblockApartmentResponse>
    {
        readonly ApplicationDbContext _db;
        public UnblockApartmentHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UnblockApartmentResponse> Handle(UnblockApartmentRequest request, CancellationToken cancellationToken)
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
