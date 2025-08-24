using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Manage.Controllers
{
    public class BaseController : Controller
    {
        protected string GetToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "JWT");
            if (claim == null)
                throw new UnauthorizedAccessException("User not logged in.");
            return claim.Value;
        }
    }
}
