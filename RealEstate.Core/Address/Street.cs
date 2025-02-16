namespace RealEstate.Core.Address
{
    public class Street
    {
        public int StreetId { get; set; }
        public string Title { get; set; }
        public int SubdistrictId { get; set; }
        public Subdistrict Subdistrict { get; set; }
        public int Id => StreetId;
    }
}
