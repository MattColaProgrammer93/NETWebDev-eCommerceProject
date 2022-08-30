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

        public async Task<IActionResult> Index(int? id)
        {
            // Number of animals to display per page
            const int NumOfAnimalsToDisplayPerPage = 3;
            // Needed to use the current page and figure out, how many animals to be skipped
            const int PageOffset = 1;
            // Set currPage to id if it has value, otherwise use 1
            int currPage = id ?? 1;

            // Commented method syntax version, same code below as query syntax
            // List<Animal> animals = _context.Animals.Skip(NumOfAnimalsToDisplayPerPage * (currPage - PageOffset))
            // .Take(NumOfAnimalsToDisplayPerPage).ToList();

            // Get all the animals in database
            List<Animal> animals = await (from animal in _context.Animals
                                          select animal)
                                          .Skip(NumOfAnimalsToDisplayPerPage * (currPage - PageOffset))
                                          .ToListAsync();
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

        [HttpPost]
        public async Task<IActionResult> Edit(Animal animalModel)
        {
            if (ModelState.IsValid)
            {
                _context.Animals.Update(animalModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{animalModel.Name} was updated successfully!";
                return RedirectToAction("Index");
            }

            return View(animalModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Animal? animalToDelete = await _context.Animals.FindAsync(id);

            if (animalToDelete == null)
            {
                return NotFound();
            }

            return View(animalToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Animal? animalToDelete = await _context.Animals.FindAsync(id);

            if (animalToDelete != null)
            {
                _context.Animals.Remove(animalToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = animalToDelete.Name + " was deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "This animal was already deleted";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Animal animalDetails = await _context.Animals.FindAsync(id);

            if (animalDetails == null)
            {
                return NotFound();
            }

            return View(animalDetails);
        }
    }
}
