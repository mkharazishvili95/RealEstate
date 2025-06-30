using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Helpers;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Apartment.Create
{
    public class CreateApartmentHandler : IRequestHandler<CreateApartmentRequest, CreateApartmentResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly ApplicationDbContext _db;

        public CreateApartmentHandler(IIdentityService identityService, ApplicationDbContext db)
        {
            _identityService = identityService;
            _db = db;
        }

        public async Task<CreateApartmentResponse> Handle(CreateApartmentRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
                return Fail(400, "UserId should not be empty.");

            var user = await _identityService.GetUserById(request.UserId);
            if (user == null)
                return Fail(404, "User not found.");

            if (user.IsBlocked)
                return Fail(400, "User is blocked.");

            var tariffForIndividualUser = await _db.Tariffs
                .FirstOrDefaultAsync(x => x.PaidService == Common.Enums.PaidService.PaidService.StandartIndividuals, cancellationToken);
            var tariffForAgent = await _db.Tariffs
                .FirstOrDefaultAsync(x => x.PaidService == Common.Enums.PaidService.PaidService.StandartAgents, cancellationToken);

            decimal calculatedPrice = TariffHelper.CalculateTariffPrice(user.Type, tariffForIndividualUser, tariffForAgent);

            if (user.Balance < calculatedPrice)
                return Fail(402, $"Insufficient balance. Apartment cost is {calculatedPrice} ₾, your balance is {user.Balance} ₾.");

            using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                user.Balance -= calculatedPrice;
                _db.Users.Update(user);

                var apartment = new Core.Apartment.Apartment
                {
                    Title = string.IsNullOrWhiteSpace(request.Title)
                        ? $"{request.ApartmentType} - {request.ApartmentDealType}"
                        : request.Title,
                    Description = request.Description,
                    Status = Common.Enums.Apartment.ApartmentStatus.Active,
                    CreateDate = DateTime.UtcNow,
                    EndDate = user.Type == Common.Enums.User.UserType.Individual
                        ? DateTime.UtcNow.AddDays(30)
                        : DateTime.UtcNow.AddDays(45),
                    UpdateDate = null,
                    DeleteDate = null,
                    UserId = request.UserId,
                    Price = request.Price,
                    CurrencyId = request.CurrencyId ?? 2,
                    AgencyId = request.AgencyId,
                    ApartmentType = request.ApartmentType,
                    ApartmentDealType = request.ApartmentDealType,
                    ApartmentState = request.ApartmentState,
                    BuildingStatus = request.BuildingStatus,
                    StreetId = request.StreetId,
                    DistrictId = request.DistrictId,
                    SubdistrictId = request.SubdistrictId,
                    CityId = request.CityId
                };

                await _db.Apartments.AddAsync(apartment, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new CreateApartmentResponse
                {
                    Success = true,
                    StatusCode = 201,
                    UserMessage = $"Apartment created and {calculatedPrice} ₾ was deducted from your balance."
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                return Fail(500, "An unexpected error occurred while creating the apartment.");
            }
        }

        private CreateApartmentResponse Fail(int code, string message) => new()
        {
            Success = false,
            StatusCode = code,
            UserMessage = message
        };
    }
}
