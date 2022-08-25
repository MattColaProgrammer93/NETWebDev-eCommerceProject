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

                LogUserIn(newUser.Email);

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Check DB for credentials
                User? u = (from user in _context.Users
                           where user.Email == loginModel.Email &&
                                 user.Password == loginModel.Password
                           select user).SingleOrDefault();
                // If exists, send to homepage
                if (u != null)
                {
                    LogUserIn(loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credentials not found");
            }
            // Return to page if no record is found, or ModelState is invalid
            return View(loginModel);
        }

        private void LogUserIn(string email)
        {
            HttpContext.Session.SetString("Email", email);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
