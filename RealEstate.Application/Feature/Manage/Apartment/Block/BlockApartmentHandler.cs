using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Apartment.Block
{
    public class BlockApartmentHandler : IRequestHandler<BlockApartmentRequest, BlockApartmentResponse>
    {
        readonly ApplicationDbContext _db;
        public BlockApartmentHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<BlockApartmentResponse> Handle(BlockApartmentRequest request, CancellationToken cancellationToken)
        {
            if (request.ApartmentId == null || request.ApartmentId <= 0)
                return new BlockApartmentResponse { Success = false, StatusCode = 400, UserMessage = "request should not be empty." };

            var apartment = await _db.Apartments
                .FirstOrDefaultAsync(x => x.ApartmentId == request.ApartmentId && x.Status != Common.Enums.Apartment.ApartmentStatus.Blocked);

            if(apartment == null)
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
    }
}
