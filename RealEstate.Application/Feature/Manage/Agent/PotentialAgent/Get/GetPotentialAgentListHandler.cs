using Microsoft.EntityFrameworkCore;
using RealEstate.Common.Enums.Apartment;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Agent.PotentialAgent.Get
{
    public class GetPotentialAgentListHandler : IRequestHandler<GetPotentialAgentListRequest, GetPotentialAgentListResponse>
    {
        private readonly ApplicationDbContext _db;

        public GetPotentialAgentListHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetPotentialAgentListResponse> Handle(GetPotentialAgentListRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Users
                .Include(u => u.Apartments)
                .AsQueryable();

            query = query.Where(u => u.Apartments != null 
            && u.Type == Common.Enums.User.UserType.Individual
            && u.Apartments.Count(a => a.Status == ApartmentStatus.Active) > 1);

            if (request.Status.HasValue)
            {
                query = request.Status.Value switch
                {
                    0 => query.Where(u => !u.IsBlocked),
                    1 => query.Where(u => u.IsBlocked),
                    _ => query
                };
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var pageSize = request.Pagination?.PageSize ?? 20;
            var page = request.Pagination?.Page ?? 1;
            var skip = (page - 1) * pageSize;

            var items = await query
                .OrderBy(u => u.IsBlocked)
                .ThenBy(u => u.FirstName)
                .Skip(skip)
                .Take(pageSize)
                .Select(u => new GetPotentialAgentListItemsResponse
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PIN = u.PIN,
                    UserName = u.UserName,
                    Type = u.Type,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Balance = u.Balance,
                    IsBlocked = u.IsBlocked,
                    BlockDate = u.BlockDate,
                    BlockReason = u.BlockReason,
                    Gender = u.Gender,
                    RegisterDate = u.RegisterDate
                })
                .ToListAsync(cancellationToken);

            return new GetPotentialAgentListResponse
            {
                Success = true,
                StatusCode = 200,
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
