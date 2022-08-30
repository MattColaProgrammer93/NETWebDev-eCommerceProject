namespace NETWebDev_eCommerceProject.Models
{
    public class AnimalCatalogViewModel
    {
        public AnimalCatalogViewModel(List<Animal> animals, int lastPage, int currentPage)
        {
            Animals = animals;
            LastPage = lastPage;
            CurrentPage = currentPage;
        }

        public List<Animal> Animals { get; private set; }

        /// <summary>
        /// The last page of the catalog. Calculated by having a
        /// total number of animals divided by animals per page.
        /// </summary>
        public int LastPage { get; private set; }
        
        /// <summary>
        /// Current page that the user is viewing
        /// </summary>
        public int CurrentPage { get; private set; }
    }
}
