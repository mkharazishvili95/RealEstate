using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Profile.Apartments
{
    public class MyApartmentsHandler : IRequestHandler<MyApartmentsRequest, MyApartmentsResponse>
    {
        readonly ApplicationDbContext _db;
        readonly IIdentityService _identityService;
        public MyApartmentsHandler(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public async Task<MyApartmentsResponse> Handle(MyApartmentsRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return new MyApartmentsResponse
                {
                    Success = false,
                    StatusCode = 400,
                    UserMessage = "UserId should not be empty."
                };
            }

            var user = await _identityService.GetUserById(request.UserId);
            if (user == null)
            {
                return new MyApartmentsResponse
                {
                    Success = false,
                    StatusCode = 404,
                    UserMessage = "User not found."
                };
            }

            var query = _db.Apartments
                .Include(x => x.Agency)
                .Where(x => x.UserId == request.UserId);

            if (request.Status.HasValue)
            {
                query = query.Where(x => x.Status == request.Status.Value);
            }

            var apartments = await query.ToListAsync(cancellationToken);

            if (!apartments.Any())
            {
                return new MyApartmentsResponse
                {
                    Success = false,
                    TotalCount = 0,
                    StatusCode = 404,
                    UserMessage = "Apartments not found."
                };
            }

            var items = apartments.Select(x => new MyApartmentsItemsResponse
            {
                ApartmentId = x.ApartmentId,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                BlockReason = x.BlockReason,
                CreateDate = x.CreateDate,
                EndDate = x.EndDate,
                UpdateDate = x.UpdateDate,
                DeleteDate = x.DeleteDate,
                Price = x.Price,
                CurrencyId = x.CurrencyId,
                AgencyId = x.AgencyId,
                AgencyName = x.Agency?.Name ?? null
            }).ToList();

            return new MyApartmentsResponse
            {
                Items = items,
                TotalCount = items.Count,
                Success = true,
                StatusCode = 200
            };
        }

    }
}
