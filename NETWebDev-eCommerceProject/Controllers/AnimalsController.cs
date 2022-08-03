using Microsoft.AspNetCore.Mvc;

namespace NETWebDev_eCommerceProject.Controllers
{
    public class AnimalsController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
