using RealEstate.Core.User;

namespace RealEstate.Application.Services
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();

    }
}
