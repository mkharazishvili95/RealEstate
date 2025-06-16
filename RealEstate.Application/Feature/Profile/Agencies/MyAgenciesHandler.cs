using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Profile.Agencies
{
    public class MyAgenciesHandler : IRequestHandler<MyAgenciesRequest, MyAgenciesResponse>
    {
        private readonly ApplicationDbContext _db;
        private readonly IIdentityService _identityService;

        public MyAgenciesHandler(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public async Task<MyAgenciesResponse> Handle(MyAgenciesRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return new MyAgenciesResponse
                {
                    Success = false,
                    StatusCode = 400,
                    UserMessage = "UserId should not be empty."
                };
            }

            var user = await _identityService.GetUserById(request.UserId);
            if (user == null)
            {
                return new MyAgenciesResponse
                {
                    Success = false,
                    StatusCode = 404,
                    UserMessage = "User not found."
                };
            }

            var query = _db.Agencies
                .Where(a => a.UserId == request.UserId);

            if (!string.IsNullOrWhiteSpace(request.AgencyName))
            {
                query = query.Where(a => a.Name.Contains(request.AgencyName));
            }

            if (!string.IsNullOrWhiteSpace(request.IdentificationNumber))
            {
                query = query.Where(a => a.IdentificationNumber == request.IdentificationNumber);
            }

            if (request.Type.HasValue)
            {
                query = query.Where(a => a.AgencyType == request.Type.Value);
            }

            var agencies = await query.ToListAsync(cancellationToken);

            if (!agencies.Any())
            {
                return new MyAgenciesResponse
                {
                    Success = false,
                    StatusCode = 404,
                    UserMessage = "Agencies not found.",
                    TotalCount = 0
                };
            }

            var items = agencies.Select(a => new MyAgenciesItemsResponse
            {
                AgencyId = a.AgencyId,
                AgencyType = a.AgencyType,
                Name = a.Name,
                LogoUrl = a.LogoUrl,
                IsApproved = a.IsApproved,
                IsDeleted = a.IsDeleted,
                DeleteReason = a.DeleteReason,
                UpdateDate = a.UpdateDate,
                DeleteDate = a.DeleteDate,
                Address = a.Address,
                IdentificationNumber = a.IdentificationNumber,
                Description = a.Description,
                Email = a.Email,
                Link = a.Link,
                PhoneNumber = a.PhoneNumber,
                CreateDate = a.CreateDate
            }).ToList();

            return new MyAgenciesResponse
            {
                Success = true,
                StatusCode = 200,
                Items = items,
                TotalCount = items.Count
            };
        }
    }
}
