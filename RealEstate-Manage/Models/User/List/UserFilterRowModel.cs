using RealEstate.Common.Enums.User;

namespace RealEstate.Models.User.List
{
    public class UserFilterRowModel
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PIN { get; set; }
        public string? UserName { get; set; }
        public UserType? Type { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? BalanceFrom { get; set; }
        public decimal? BalanceTo { get; set; }
        public bool? IsBlocked { get; set; }
        public DateTime? BlockDateFrom { get; set; }
        public DateTime? BlockDateTo { get; set; }
        public Gender? Gender { get; set; }
        public int? PageSize { get; set; } = 20;
        public int? Page { get; set; } = 1;
        public DateTime? RegisterDateFrom { get; set; }
        public DateTime? RegisterDateTo { get; set; }
        //public Order? Order { get; set; }
    }
    //public enum Order
    //{
    //    RegisterDateAsc,
    //    RegisterDateDesc,
    //    BalanceAsc,
    //    BalanceDesc
    //}
}
