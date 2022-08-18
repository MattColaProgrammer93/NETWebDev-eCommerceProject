using System.ComponentModel.DataAnnotations;

namespace NETWebDev_eCommerceProject.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string? UserName { get; set; } 

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
