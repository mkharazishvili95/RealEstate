namespace RealEstate_Manage.Models.Apartment.List
{
    public class ApartmentListViewModel
    {
        public ApartmentFilterRowModel Filter { get; set; } = new();
        public List<ApartmentTableRowViewModel> Apartments { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageSize => Filter.PageSize ?? 20;
        public int Page => Filter.Page ?? 1;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
