namespace RealEstate.Common.Models
{
    public class Pagination
    {
        public int? PageSize { get; set; } = 20;
        public int? Page { get; set; } = 1;
        public int Skip => (Page!.Value - 1) * PageSize!.Value;
    }
}
