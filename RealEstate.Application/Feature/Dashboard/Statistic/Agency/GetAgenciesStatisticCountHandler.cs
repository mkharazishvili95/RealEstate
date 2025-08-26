using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Dashboard.Statistic.Agency
{
    public class GetAgenciesStatisticCountHandler : IRequestHandler<GetAgenciesStatisticCountRequest, GetAgenciesStatisticCountResponse>
    {
        readonly ApplicationDbContext _db;
        public GetAgenciesStatisticCountHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetAgenciesStatisticCountResponse> Handle(GetAgenciesStatisticCountRequest request, CancellationToken cancellationToken)
        {
            var count = await _db.Agencies
                .Where(a => a.CreateDate >= request.DateFrom && a.CreateDate <= request.DateTo)
                .CountAsync(cancellationToken);

            return new GetAgenciesStatisticCountResponse
            {
                TotalCount = count
            };
        }
    }
}
