using RealEstate.Application.Services;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.Apartment.FavoriteApartment
{
    public class FavoriteApartmentsHandler : IRequestHandler<FavoriteApartmenstRequest, FavoriteApartmentsResponse>
    {
        readonly ApplicationDbContext _db;
        readonly IIdentityService _identityService;
        public FavoriteApartmentsHandler(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }
        //TODO: Muust finish
        public async Task<FavoriteApartmentsResponse> Handle(FavoriteApartmenstRequest request, CancellationToken cancellationToken)
        {
            var getUser = await _identityService.GetUserById("justfortesting");
            return null;
        }
    }
}
