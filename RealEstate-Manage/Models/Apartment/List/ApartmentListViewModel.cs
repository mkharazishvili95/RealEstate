namespace RealEstate_Manage.Models.Apartment.List
{
    public class ApartmentListViewModel
    {
        public ApartmentFilterRowModel Filter { get; set; } = new();
        public List<ApartmentTableRowViewModel> Apartments { get; set; } = new();
    }
}
