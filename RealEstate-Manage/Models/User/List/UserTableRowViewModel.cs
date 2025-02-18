using RealEstate.Common.Enums.User;
using System;

namespace RealEstate.Models.User.List
{
    public class UserTableRowViewModel
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
        public bool? IsBlocked { get; set; }
        public DateTime? BlockDate { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? RegisterDate { get; set; }
    }
}
