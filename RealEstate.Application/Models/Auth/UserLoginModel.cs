using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.Models.Auth
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
