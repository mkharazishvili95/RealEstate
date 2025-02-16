using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Services
{
    public class AgencyService : IAgencyService
    {
        readonly ApplicationDbContext _db;
        public AgencyService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Core.Agency.Agency> GetAgencyById(int id) => await _db.Agencies.FirstOrDefaultAsync(x => x.AgencyId == id);
    }
}
