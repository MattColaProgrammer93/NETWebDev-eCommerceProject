using Microsoft.AspNetCore.Mvc;

namespace NETWebDev_eCommerceProject.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
