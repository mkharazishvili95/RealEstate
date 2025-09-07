using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Job.Apartment.Expire
{
    public class ExpireApartmentsCommandHandler : IRequestHandler<ExpireApartmentsCommand, Unit>
    {
        readonly ApplicationDbContext _db;
        public ExpireApartmentsCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(ExpireApartmentsCommand request, CancellationToken cancellationToken)
        {
            var expiredApartments = await _db.Apartments
                .Where(a => a.Status == Common.Enums.Apartment.ApartmentStatus.Active && a.EndDate <= DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            if (expiredApartments.Any())
            {
                foreach(var apartment in expiredApartments)
                {
                    apartment.Status = Common.Enums.Apartment.ApartmentStatus.Expired;
                }
                await _db.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
