using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Helpers;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Apartment.List
{
    public class GetApartmentListForManageHandler : IRequestHandler<GetApartmentListForManageRequest, GetApartmentListForManageResponse>
    {
        private readonly ApplicationDbContext _database;

        public GetApartmentListForManageHandler(ApplicationDbContext database)
        {
            _database = database;
        }

        public async Task<GetApartmentListForManageResponse> Handle(GetApartmentListForManageRequest request, CancellationToken cancellationToken)
        {
            var apartments = _database.Apartments
                .Include(a => a.User)
                .Where(request.WhereClause())
                .Select(a => new GetApartmentListForManageItemsResponse
                {
                    ApartmentId = a.ApartmentId,
                    Title = a.Title,
                    Status = a.Status,
                    CreateDate = a.CreateDate,
                    UpdateDate = a.UpdateDate,
                    DeleteDate = a.DeleteDate,
                    UserPin = a.User != null  && a.User.UserId != null ? a.User.PIN : null,
                    Price = a.Price,
                    CurrencyId = a.CurrencyId,
                    AgencyId = a.AgencyId
                });

            var paginatedResult = await apartments.ToPaginatedListAsync(request);
            paginatedResult.StatusCode = StatusCodes.Status200OK;
            paginatedResult.Success = true;

            return new GetApartmentListForManageResponse
            {
                ApartmentListForManage = paginatedResult.Items,
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                TotalCount = paginatedResult.Items.Count()
            };
        }
    }
}
