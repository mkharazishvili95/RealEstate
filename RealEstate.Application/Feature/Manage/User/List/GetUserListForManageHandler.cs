using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Helpers;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.User.List
{
    public class GetUserListForManageHandler : IRequestHandler<GetUserListForManageRequest, GetUserListForManageResponse>
    {
        private readonly ApplicationDbContext _database;

        public GetUserListForManageHandler(ApplicationDbContext database)
        {
            _database = database;
        }

        public async Task<GetUserListForManageResponse> Handle(GetUserListForManageRequest request, CancellationToken cancellationToken)
        {
            var users = _database.Users
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

            var paginatedResult = await users.ToPaginatedListAsync(request);
            paginatedResult.StatusCode = StatusCodes.Status200OK;
            paginatedResult.Success = true;

            return new GetUserListForManageResponse
            {
                UserListForManage = paginatedResult.Items,
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                TotalCount = paginatedResult.Items.Count()
            };
        }
    }
}
