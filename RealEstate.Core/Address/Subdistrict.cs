namespace RealEstate.Core.Address
{
    public class Subdistrict
    {
        public int SubdistrictId { get; set; }
        public string Title { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int Id => SubdistrictId;
        public List<Street> Streets { get; set; }
    }
}
