using RealEstate.Application.Models.Auth;
using RealEstate.Core.User;

namespace RealEstate.Application.Services
{
    public interface IUserService
    {
        User? Authenticate(string userName, string password);
        void Update(User user);
        (bool Success, string ErrorMessage) Register(UserRegisterModel model);
        bool EmailExists(string email);
        bool PhoneNumberExists(string phoneNumber);
    }
}
