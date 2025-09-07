using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Common.Enums.Apartment;
using RealEstate.Common.Models;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.User.Apartment.Get
{
    public class GetUserApartmentsHandler : IRequestHandler<GetUserApartmentsRequest, GetUserApartmentsResponse>
    {
        private readonly ApplicationDbContext _db;
        private readonly IIdentityService _identityService;

        public GetUserApartmentsHandler(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public async Task<GetUserApartmentsResponse> Handle(GetUserApartmentsRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                return new GetUserApartmentsResponse
                {
                    TotalCount = 0,
                    Items = new List<GetUserApartmentsItemsResponse>(),
                    StatusCode = 400,
                    Success = false,
                    UserMessage = "UserId is required."
                };
            }

            var user = await _identityService.GetUserById(request.UserId);
            if (user == null)
            {
                return new GetUserApartmentsResponse
                {
                    TotalCount = 0,
                    Items = new List<GetUserApartmentsItemsResponse>(),
                    StatusCode = 404,
                    Success = false,
                    UserMessage = "User not found."
                };
            }

            var pagination = request.Pagination ?? new Pagination();

            var query = _db.Apartments
                .Include(x => x.Agency)
                .Where(a => a.UserId == request.UserId && a.Status == ApartmentStatus.Active);

            var totalCount = await query.CountAsync(cancellationToken);

            if (totalCount == 0)
            {
                return new GetUserApartmentsResponse
                {
                    TotalCount = 0,
                    Items = new List<GetUserApartmentsItemsResponse>(),
                    StatusCode = 200,
                    Success = true,
                    UserMessage = "No apartments found for this user."
                };
            }

            var items = await query
                .OrderByDescending(a => a.CreateDate)
                .Skip(pagination.Skip)
                .Take(pagination.PageSize.Value)
                .Select(a => new GetUserApartmentsItemsResponse
                {
                    ApartmentId = a.ApartmentId,
                    Title = a.Title,
                    Description = a.Description,
                    CreateDate = a.CreateDate,
                    EndDate = a.EndDate,
                    UpdateDate = a.UpdateDate,
                    Price = a.Price,
                    CurrencyId = a.CurrencyId,
                    AgencyId = a.AgencyId,
                    AgencyName = a.Agency != null ? a.Agency.Name : null,
                    AgencyType = a.Agency != null ? a.Agency.AgencyType : null,
                    AgencyLogoUrl = a.Agency != null ? a.Agency.LogoUrl : null,
                    ApartmentType = a.ApartmentType,
                    ApartmentDealType = a.ApartmentDealType,
                    ApartmentState = a.ApartmentState,
                    BuildingStatus = a.BuildingStatus,
                    StreetId = a.StreetId,
                    DistrictId = a.DistrictId,
                    SubdistrictId = a.SubdistrictId,
                    CityId = a.CityId,
                })
                .ToListAsync(cancellationToken);

            return new GetUserApartmentsResponse
            {
                TotalCount = totalCount,
                Items = items,
                StatusCode = 200,
                Success = true,
                UserMessage = "User apartments retrieved successfully."
            };
        }
    }
}
