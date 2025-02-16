using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Manage.Apartment.List
{
    public class GetApartmentListForManageHandler : IRequestHandler<GetApartmentListForManageRequest, GetApartmentListForManageResponse>
    {
        readonly ApplicationDbContext _db;
        public GetApartmentListForManageHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetApartmentListForManageResponse> Handle(GetApartmentListForManageRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Apartments.AsQueryable();

            if (request.ApartmentId.HasValue)
                query = query.Where(a => a.ApartmentId == request.ApartmentId);

            if (!string.IsNullOrEmpty(request.Title))
                query = query.Where(a => a.Title.ToUpper().Contains(request.Title.ToUpper()));

            if (request.Status.HasValue)
                query = query.Where(a => a.Status == request.Status);

            if (request.CreateDateFrom.HasValue)
                query = query.Where(a => a.CreateDate >= request.CreateDateFrom);
            if (request.CreateDateTo.HasValue)
                query = query.Where(a => a.CreateDate <= request.CreateDateTo);

            if (request.UpdateDateFrom.HasValue)
                query = query.Where(a => a.UpdateDate >= request.UpdateDateFrom);
            if (request.UpdateDateTo.HasValue)
                query = query.Where(a => a.UpdateDate <= request.UpdateDateTo);

            if (request.DeleteDateFrom.HasValue)
                query = query.Where(a => a.DeleteDate >= request.DeleteDateFrom);
            if (request.DeleteDateTo.HasValue)
                query = query.Where(a => a.DeleteDate <= request.DeleteDateTo);

            if (!string.IsNullOrEmpty(request.UserId))
                query = query.Where(a => a.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.UserPin))
                query = query.Where(a => a.User.PIN.Contains(request.UserPin));

            if (request.PriceFrom.HasValue)
                query = query.Where(a => a.Price >= request.PriceFrom);
            if (request.PriceTo.HasValue)
                query = query.Where(a => a.Price <= request.PriceTo);

            if (request.UnitPriceFrom.HasValue)
                query = query.Where(a => a.UnitPrice >= request.UnitPriceFrom);
            if (request.UnitPriceTo.HasValue)
                query = query.Where(a => a.UnitPrice <= request.UnitPriceTo);

            if (request.CurrencyId.HasValue)
                query = query.Where(a => a.CurrencyId == request.CurrencyId);

            if (request.AgencyId.HasValue)
                query = query.Where(a => a.AgencyId == request.AgencyId);

            int page = request.Page ?? 1;
            int pageSize = request.PageSize ?? 20;
            int skip = (page - 1) * pageSize;

            var apartments = await query
                .OrderBy(a => a.ApartmentId)
                .Skip(skip)
                .Take(pageSize)
                .Select(a => new GetApartmentListForManageItemsResponse
                {
                    ApartmentId = a.ApartmentId,
                    Title = a.Title,
                    Status = a.Status,
                    CreateDate = a.CreateDate,
                    UpdateDate = a.UpdateDate,
                    DeleteDate = a.DeleteDate,
                    UserPin = a.User.PIN,
                    Price = a.Price,
                    UnitPrice = a.UnitPrice,
                    CurrencyId = a.CurrencyId,
                    AgencyId = a.AgencyId
                })
                .ToListAsync(cancellationToken);

            return new GetApartmentListForManageResponse
            {
                TotalCount = apartments.Count,
                Pagination = new Common.Models.Pagination { Page = page, PageSize = pageSize },
                ApartmentListForManage = apartments,
                StatusCode = 200,
                Success = true
            };
        }
    }
}
