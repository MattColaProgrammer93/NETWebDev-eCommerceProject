using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            // Get all the animals in database
            // List<Animal> animals = _context.Animals.ToList();
            List<Animal> animals = await (from animal in _context.Animals
                                          select animal).ToListAsync();
            // Show them to the catalog in the homepage
            return View(animals);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Animal a)
        {
            if (ModelState.IsValid)
            {
                // For async code in the tutorial
                // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0#asynchronous-code 
                // Add to DB
                _context.Animals.Add(a);            // Prepares insert
                await _context.SaveChangesAsync();  // Executes pending insert

                // Show success message on page
                ViewData["Message"] = $"{a.Name} was made and added successfully!";

                return View();
            }

            return View(a);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Animal? animalToEdit = await _context.Animals.FindAsync(id);

            if (animalToEdit == null)
            {
                return NotFound();
            }

            return View(animalToEdit);
        }
    }
}
