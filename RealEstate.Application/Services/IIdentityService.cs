namespace RealEstate.Application.Services
{
    public interface IIdentityService
    {
        Task<Core.User.User> GetUserById(string id);
    }
}
