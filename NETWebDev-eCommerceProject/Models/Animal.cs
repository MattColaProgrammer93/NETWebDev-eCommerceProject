using System.ComponentModel.DataAnnotations;

namespace NETWebDev_eCommerceProject.Models

{

    /// <summary>
    /// Represents a individual animal
    /// </summary>
    public class Animal
    {
        /// <summary>
        /// The unique indentifier for each animal id 
        /// </summary>
        [Key]
        public int AnimalId { get; set; }

        /// <summary>
        /// The animal's official name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The animal's type
        /// </summary>
        [Required]
        public string Type { get; set; }

    }

    /// <summary>
    /// A single animal that has been added to the user's deck cookie
    /// </summary>
    public class DeckAnimalViewModel
    {
        public int AnimalId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
