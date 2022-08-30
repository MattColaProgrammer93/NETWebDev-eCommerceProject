using Microsoft.AspNetCore.Mvc;
using NETWebDev_eCommerceProject.Data;
using NETWebDev_eCommerceProject.Models;
using Newtonsoft.Json;

namespace NETWebDev_eCommerceProject.Controllers
{
    public class DeckController : Controller
    {
        private readonly AnimalCreationContext _context;
        private const string Deck = "Deck";

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

            DeckAnimalViewModel deckAnimal = new()
            {
                AnimalId = animalToAdd.AnimalId,
                Name = animalToAdd.Name,
                Type = animalToAdd.Type
            };

            List<DeckAnimalViewModel> deckAnimals = GetExistingDeckData();
            deckAnimals.Add(deckAnimal);
            WriteDeckCookies(deckAnimals);

            // Todo: Add item to a deck cookie
            TempData["Message"] = "Animal added to deck";
            return RedirectToAction("Index", "Animals");
        }

        /// <summary>
        /// Writes the cookies of the deckAnimals into strings by using JSON
        /// </summary>
        /// <param name="deckAnimals">The list of deck of animals view model</param>
        private void WriteDeckCookies(List<DeckAnimalViewModel> deckAnimals)
        {
            string cookieData = JsonConvert.SerializeObject(deckAnimals);

            HttpContext.Response.Cookies.Append(Deck, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
        }

        /// <summary>
        /// Return the current list of animals in the users deck cookie.
        /// If there is no cookie, an empty list will be returned
        /// </summary>
        /// <returns></returns>
        private List<DeckAnimalViewModel> GetExistingDeckData()
        {
            string? cookie = HttpContext.Request.Cookies[Deck];
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return new List<DeckAnimalViewModel>();
            }

            return JsonConvert.DeserializeObject<List<DeckAnimalViewModel>>(cookie);
        }
    
        public IActionResult Summary()
        {
            // Read the deck data and convert to list of view model
            List<DeckAnimalViewModel> deckAnimals = GetExistingDeckData();
            return View(deckAnimals);
        }

        public IActionResult Remove(int id)
        {
            List<DeckAnimalViewModel> deckAnimals = GetExistingDeckData();

            DeckAnimalViewModel? targetAnimal = 
                deckAnimals.Where(a => a.AnimalId == id).FirstOrDefault();

            deckAnimals.Remove(targetAnimal);

            WriteDeckCookies(deckAnimals);

            return RedirectToAction("Summary");
        }
    }
}
