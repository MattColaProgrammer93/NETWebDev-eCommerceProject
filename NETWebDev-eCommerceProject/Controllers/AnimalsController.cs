using Microsoft.AspNetCore.Mvc;
using NETWebDev_eCommerceProject.Data;
using NETWebDev_eCommerceProject.Models;

namespace NETWebDev_eCommerceProject.Controllers
{
    public class AnimalsController : Controller
    {
        // Do not reassign this
        private readonly AnimalCreationContext _context;

        public AnimalsController(AnimalCreationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Animal a)
        {
            if (ModelState.IsValid)
            {
                // Add to DB
                _context.Animals.Add(a); // Prepares insert
                _context.SaveChanges(); // Executes pending insert

                // Show success message on page
                ViewData["Message"] = $"{a.Name} was made and added successfully!";

                return View();
            }

            return View(a);
        }
    }
}
