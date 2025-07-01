using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Address;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        readonly ApplicationDbContext _db;
        public AddressRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public City? GetCityById(int id)
        {
            return _db.Cities.AsNoTracking().Where(x => x.CityId == id).FirstOrDefault();
        }
        public Subdistrict? GetSubdistrictById(int id)
        {
            return _db.Subdistricts.AsNoTracking().Where(x => x.SubdistrictId == id).FirstOrDefault();
        }
        public District? GetDistrictById(int id)
        {
            return _db.Districts.AsNoTracking().Where(x => x.DistrictId == id).FirstOrDefault();
        }
        public Street? GetStreetById(int id)
        {
            return _db.Streets.AsNoTracking().Where(x => x.StreetId == id).FirstOrDefault();
        }
    }
}
