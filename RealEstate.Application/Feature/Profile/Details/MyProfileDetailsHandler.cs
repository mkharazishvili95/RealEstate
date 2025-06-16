using RealEstate.Application.Services;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Profile.Details
{
    public class MyProfileDetailsHandler : IRequestHandler<MyProfileDetailsRequest, MyProfileDetailsResponse>
    {
        readonly ApplicationDbContext _db;
        readonly IIdentityService _identityService;
        public MyProfileDetailsHandler(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public async Task<MyProfileDetailsResponse> Handle(MyProfileDetailsRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
                return new MyProfileDetailsResponse() { Success = false, StatusCode = 400, UserMessage = "Request should not be empty."};

            var user = await _identityService.GetUserById(request.UserId);

            if (user == null)
            {
                return new MyProfileDetailsResponse() { Success = false, StatusCode = 400, UserMessage = "User not found." };
            }
            else
            {
                return new MyProfileDetailsResponse()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Balance = user.Balance,
                    PIN = user.PIN,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    IsBlocked = user.IsBlocked,
                    BlockDate = user.BlockDate,
                    RegisterDate = user.RegisterDate,
                    Type = user.Type,
                    Gender = user.Gender,
                    Success = true,
                    StatusCode = 200
                };
            }
        }
    }
}
