namespace RealEstate.Core.Address
{
    public interface IAddressRepository
    {
        City? GetCityById(int id);
        Subdistrict? GetSubdistrictById(int id);
        District? GetDistrictById(int id);
        Street? GetStreetById(int id);
    }
}
