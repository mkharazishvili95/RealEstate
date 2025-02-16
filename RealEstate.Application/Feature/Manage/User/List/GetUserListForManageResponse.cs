using RealEstate.Application.Models;
using RealEstate.Common.Enums.User;

namespace RealEstate.Application.Feature.Manage.User.List
{
    public class GetUserListForManageResponse : ResponseBaseModel
    {
        public List<GetUserListForManageItemsResponse> UserListForManage { get; set; } = new();
        public int? PageSize { get; set; } = 20;
        public int? Page { get; set; } = 1;
        public int? Skip => (Page!.Value - 1) * PageSize!.Value;
    }
    public class GetUserListForManageItemsResponse
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PIN { get; set; }
        public string? UserName { get; set; }
        public UserType? Type { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Balance { get; set; }
        public bool? IsBlocked { get; set; } = false;
        public DateTime? BlockDate { get; set; }
        public Gender? Gender { get; set; }
    }
}
