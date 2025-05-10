using Microsoft.AspNetCore.Mvc.Rendering;

namespace RealEstate_Manage.Models.Agency.List
{
    public class AgencyListViewModel
    {
        public AgencyFilterRowModel Filter { get; set; } = new();
        public List<AgencyTableRowViewModel> Agencies { get; set; } = new();
        public List<SelectListItem> AgencyTypeOptions { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageSize => Filter.PageSize ?? 20;
        public int Page => Filter.Page ?? 1;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
