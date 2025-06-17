using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstate.Common.Enums.Apartment;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Apartment.List
{
    public class GetApartmentListForManageHandler : IRequestHandler<GetApartmentListForManageRequest, GetApartmentListForManageResponse>
    {
        private readonly ApplicationDbContext _db;

        public GetApartmentListForManageHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetApartmentListForManageResponse> Handle(GetApartmentListForManageRequest request, CancellationToken cancellationToken)
        {
            var apartments = _db.Apartments
                .Include(a => a.User)
                .Where(request.WhereClause())
                .OrderByDescending(a => a.Status == ApartmentStatus.Active)
                .ThenByDescending(a => a.CreateDate)
                .Select(a => new GetApartmentListForManageItemsResponse
                {
                    ApartmentId = a.ApartmentId,
                    Title = a.Title,
                    Status = a.Status,
                    CreateDate = a.CreateDate,
                    UpdateDate = a.UpdateDate,
                    DeleteDate = a.DeleteDate,
                    UserPin = a.User != null && a.User.UserId != null ? a.User.PIN : null,
                    Price = a.Price,
                    CurrencyId = a.CurrencyId,
                    AgencyId = a.AgencyId
                });

            var totalCount = await apartments.CountAsync(cancellationToken);

            var paginatedResult = await apartments
                .Skip((request.Page.Value - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .ToListAsync(cancellationToken);

            return new GetApartmentListForManageResponse
            {
                ApartmentListForManage = paginatedResult,
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                TotalCount = totalCount
            };

        }
    }
}
