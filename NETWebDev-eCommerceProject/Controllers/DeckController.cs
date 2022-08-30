using Microsoft.AspNetCore.Mvc;
using NETWebDev_eCommerceProject.Data;
using NETWebDev_eCommerceProject.Models;

namespace NETWebDev_eCommerceProject.Controllers
{
    public class DeckController : Controller
    {
        // Do not reassign this
        private readonly AnimalCreationContext _context;

        public DeckController(AnimalCreationContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Animal? animalToAdd = _context.Animals.Where(a => a.AnimalId == id).SingleOrDefault();

            if (animalToAdd == null)
            {
                // Animal with specified id does not exist
                TempData["Message"] = "Sorry, the animal no longer exists";
                return RedirectToAction("Index", "Animals");
            }

            // Todo: Add item to a deck cookie
            TempData["Message"] = "Animal added to deck";
            return RedirectToAction("Index", "Animals");
        }
    }
}
