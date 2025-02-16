using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.User.List
{
    public class GetUserListForManageHandler : IRequestHandler<GetUserListForManageRequest, GetUserListForManageResponse>
    {
        readonly ApplicationDbContext _db;

        public GetUserListForManageHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetUserListForManageResponse> Handle(GetUserListForManageRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Users.AsQueryable();

            if (!string.IsNullOrEmpty(request.UserId))
                query = query.Where(u => u.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.FirstName))
                query = query.Where(u => u.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));

            if (!string.IsNullOrEmpty(request.PIN))
                query = query.Where(u => u.PIN.Contains(request.PIN));

            if (!string.IsNullOrEmpty(request.LastName))
                query = query.Where(u => u.LastName.ToUpper().Contains(request.LastName.ToUpper()));

            if (!string.IsNullOrEmpty(request.UserName))
                query = query.Where(u => u.UserName.ToUpper().Contains(request.UserName.ToUpper()));

            if (!string.IsNullOrEmpty(request.Email))
                query = query.Where(u => u.Email.ToUpper().Contains(request.Email.ToUpper()));

            if (request.Type.HasValue)
                query = query.Where(u => (int)u.Type == (int)request.Type);

            if (request.IsBlocked.HasValue)
                query = query.Where(u => u.IsBlocked == request.IsBlocked);

            if (request.BlockDateFrom.HasValue)
                query = query.Where(u => u.BlockDate >= request.BlockDateFrom);

            if (request.BlockDateTo.HasValue)
                query = query.Where(u => u.BlockDate <= request.BlockDateTo);

            if (request.BalanceFrom.HasValue)
                query = query.Where(u => u.Balance >= request.BalanceFrom);

            if (request.BalanceTo.HasValue)
                query = query.Where(u => u.Balance >= request.BalanceTo);

            if (request.Gender.HasValue)
                query = query.Where(u => (int)u.Gender == (int)request.Gender);

            int page = request.Page ?? 1;
            int pageSize = request.PageSize ?? 20;
            int skip = (page - 1) * pageSize;

            var users = await query
                .OrderBy(u => u.UserId)
                .Skip(skip)
                .Take(pageSize)
                .Select(u => new GetUserListForManageItemsResponse
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PIN = u.PIN,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Balance = u.Balance,
                    IsBlocked = u.IsBlocked,
                    BlockDate = u.BlockDate,
                    Gender = u.Gender,
                    Type = u.Type
                })
                .ToListAsync(cancellationToken);

            return new GetUserListForManageResponse
            {
                TotalCount = users.Count,
                Pagination = new Common.Models.Pagination { Page = page, PageSize = pageSize },
                UserListForManage = users,
                StatusCode = 200,
                Success = true
            };
        }
    }
}
