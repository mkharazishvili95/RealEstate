using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Services
{
    public class IdentityService : IIdentityService
    {
        readonly ApplicationDbContext _db;
        public IdentityService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Core.User.User> GetUserById(string id) => await _db.Users.FirstOrDefaultAsync(x => x.UserId == id);

    }
}
