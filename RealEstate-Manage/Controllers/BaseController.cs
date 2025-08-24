using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Manage.Controllers
{
    public class BaseController : Controller
    {
        protected string GetToken()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("User not logged in.");
            return token;
        }
    }
}
