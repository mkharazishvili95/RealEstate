using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.User.List
{
    public class GetUserListForManageHandler : IRequestHandler<GetUserListForManageRequest, GetUserListForManageResponse>
    {
        private readonly ApplicationDbContext _db;

        public GetUserListForManageHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetUserListForManageResponse> Handle(GetUserListForManageRequest request, CancellationToken cancellationToken)
        {
            var users = _db.Users
                .Where(request.WhereClause())
                .Select(x => new GetUserListForManageItemsResponse
                {
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PIN = x.PIN,
                    UserName = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Balance = x.Balance,
                    IsBlocked = x.IsBlocked,
                    BlockDate = x.BlockDate,
                    Gender = x.Gender,
                    Type = x.Type,
                    RegisterDate = x.RegisterDate
                });

            var totalCount = await users.CountAsync(cancellationToken);

            var paginatedResult = await users
                .Skip((request.Page.Value - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .ToListAsync(cancellationToken);

            return new GetUserListForManageResponse
            {
                UserListForManage = paginatedResult,
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                TotalCount = totalCount
            };
        }
    }
}
