using RealEstate.Models.User.List;

namespace RealEstate_Manage.Models.User.List
{
    public class UserListViewModel
    {
        public UserFilterRowModel Filter { get; set; } = new();
        public List<UserTableRowViewModel> Users { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageSize => Filter.PageSize ?? 20;
        public int Page => Filter.Page ?? 1;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
