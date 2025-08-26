using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Dashboard.Statistic.Apartment
{
    public class GetApartmentsStatisticCountHandler : IRequestHandler<GetApartmentsStatisticCountRequest, GetApartmentsStatisticCountResponse>
    {
        readonly ApplicationDbContext _db;
        public GetApartmentsStatisticCountHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetApartmentsStatisticCountResponse> Handle(GetApartmentsStatisticCountRequest request, CancellationToken cancellationToken)
        {
            var count = await _db.Apartments
                .Where(a => a.CreateDate >= request.DateFrom && a.CreateDate <= request.DateTo)
                .CountAsync(cancellationToken);

            return new GetApartmentsStatisticCountResponse
            {
                TotalCount = count
            };
        }
    }
}
