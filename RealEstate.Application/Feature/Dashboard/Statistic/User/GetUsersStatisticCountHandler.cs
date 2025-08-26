using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Dashboard.Statistic.User
{
    public class GetUsersStatisticCountHandler : IRequestHandler<GetUsersStatisticCountRequest, GetUsersStatisticCountResponse>
    {
        readonly ApplicationDbContext _db;
        public GetUsersStatisticCountHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetUsersStatisticCountResponse> Handle(GetUsersStatisticCountRequest request, CancellationToken cancellationToken)
        {
            var count = await _db.Users
                .Where(a => a.RegisterDate >= request.DateFrom && a.RegisterDate <= request.DateTo)
                .CountAsync(cancellationToken);

            return new GetUsersStatisticCountResponse
            {
                TotalCount = count
            };
        }
    }
}
