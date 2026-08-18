using System.ComponentModel.DataAnnotations;

namespace SymphonyPortal.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 4,
         ErrorMessage = "Password must be between 4 and 20 characters")]
        public string Password { get; set; }
    }
}