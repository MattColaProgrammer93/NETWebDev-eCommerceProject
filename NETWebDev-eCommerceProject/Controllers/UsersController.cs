using Microsoft.AspNetCore.Mvc;
using NETWebDev_eCommerceProject.Data;
using NETWebDev_eCommerceProject.Models;

namespace NETWebDev_eCommerceProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly AnimalCreationContext _context;

        public UsersController(AnimalCreationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // Map the RegisterViewModel data to User object
                User newUser = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }
    }
}
