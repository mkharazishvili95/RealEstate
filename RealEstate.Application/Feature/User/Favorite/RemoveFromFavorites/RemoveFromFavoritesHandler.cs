using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Feature.User.Favorite.RemoveFromFavorites
{
    public class RemoveFromFavoritesHandler : IRequestHandler<RemoveFromFavoritesRequest, RemoveFromFavoritesResponse>
    {
        readonly ApplicationDbContext _db;
        readonly IIdentityService _identityService;

        public RemoveFromFavoritesHandler(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public async Task<RemoveFromFavoritesResponse> Handle(RemoveFromFavoritesRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return new RemoveFromFavoritesResponse { StatusCode = 400, Success = false, UserMessage = "Request is required" };

            var user = await _identityService.GetUserById(request.UserId);
            if (user == null)
                return new RemoveFromFavoritesResponse { StatusCode = 404, Success = false, UserMessage = "User not found" };

            var favorite = await _db.UserFavoriteApartments
                .FirstOrDefaultAsync(x => x.UserId == user.UserId && x.ApartmentId == request.ApartmentId, cancellationToken);

            if (favorite == null)
                return new RemoveFromFavoritesResponse { StatusCode = 404, Success = false, UserMessage = "Favorite not found" };

            _db.UserFavoriteApartments.Remove(favorite);
            await _db.SaveChangesAsync(cancellationToken);

            return new RemoveFromFavoritesResponse
            {
                StatusCode = 200,
                Success = true,
                UserMessage = "Apartment removed from favorites successfully"
            };
        }
    }
}
