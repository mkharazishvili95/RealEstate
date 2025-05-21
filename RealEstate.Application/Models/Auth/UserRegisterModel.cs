using System.ComponentModel.DataAnnotations;
using RealEstate.Common.Enums.Auth;
using RealEstate.Common.Enums.User;

namespace RealEstate.Application.Models.Auth
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public RegistrationMethod RegistrationMethod { get; set; }
    }
}
