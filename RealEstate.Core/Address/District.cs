namespace RealEstate.Core.Address
{
    public class District
    {
        public int DistrictId { get; set; }
        public string Title { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public List<Subdistrict> Subdistricts { get; set; }
        public int Id => DistrictId;
    }
}
