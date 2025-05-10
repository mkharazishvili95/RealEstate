using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
          (agency, user) => new { Agency = agency, User = user });

            if (!string.IsNullOrEmpty(request.OwnerPIN))
                agencies = agencies.Where(x => x.User.PIN.Contains(request.OwnerPIN));

            var projected = agencies.Select(x => new GetAgencyListForManageItemsResponse
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
                OwnerPIN = x.User.PIN,
                CreateDate = x.Agency.CreateDate
            });

            var totalCount = await projected.CountAsync(cancellationToken);

            var paginatedResult = await projected
                .Skip((request.Page.Value - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .ToListAsync(cancellationToken);

            return new GetAgencyListForManageResponse
            {
                AgencyListForManage = paginatedResult,
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                TotalCount = totalCount
            };
        }
    }
}
