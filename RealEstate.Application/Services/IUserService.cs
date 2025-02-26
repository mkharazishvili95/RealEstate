using RealEstate.Application.Models.Auth;
using RealEstate.Core.User;

namespace RealEstate.Application.Services
{
    public interface IUserService
    {
        User? Authenticate(string email, string password);
        void Update(User user);
        bool Register(UserRegisterModel user);
        bool EmailExists(string email);
    }
}
