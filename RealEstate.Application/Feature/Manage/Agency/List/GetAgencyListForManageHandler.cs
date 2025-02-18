using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Helpers;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Agency.List
{
    public class GetAgencyListForManageHandler : IRequestHandler<GetAgencyListForManageRequest, GetAgencyListForManageResponse>
    {
        private readonly ApplicationDbContext _database;

        public GetAgencyListForManageHandler(ApplicationDbContext database)
        {
            _database = database;
        }

        public async Task<GetAgencyListForManageResponse> Handle(GetAgencyListForManageRequest request, CancellationToken cancellationToken)
        {
            var agencies = _database.Agencies
                .Where(request.WhereClause())
                .Join(_database.Users,
                      agency => agency.UserId,
                      user => user.UserId,
                      (agency, user) => new { Agency = agency, User = user })
                .Select(x => new GetAgencyListForManageItemsResponse
                {
                    AgencyId = x.Agency.AgencyId,
                    AgencyType = x.Agency.AgencyType,
                    Name = x.Agency.Name,
                    IsApproved = x.Agency.IsApproved,
                    IsDeleted = x.Agency.IsDeleted,
                    IdentificationNumber = x.Agency.IdentificationNumber,
                    Email = x.Agency.Email,
                    PhoneNumber = x.Agency.PhoneNumber,
                    UpdateDate = x.Agency.UpdateDate,
                    DeleteDate = x.Agency.DeleteDate,
                    UserId = x.Agency.UserId,
                    UserPin = x.User.PIN,
                    CreateDate = x.Agency.CreateDate
                });

            var paginatedResult = await agencies.ToPaginatedListAsync(request);
            paginatedResult.StatusCode = StatusCodes.Status200OK;
            paginatedResult.Success = true;

            return new GetAgencyListForManageResponse
            {
                AgencyListForManage = paginatedResult.Items,
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                TotalCount = paginatedResult.Items.Count()
            };
        }
    }
}
