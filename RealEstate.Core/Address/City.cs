using RealEstate.Common.Enums.Address;

namespace RealEstate.Core.Address
{
    public class City
    {
        public int CityId { get; set; }
        public string Title { get; set; }
        public int Id => CityId;
        public CityType? CityType { get; set; }
        public List<District> Districts { get; set; }
    }
}
