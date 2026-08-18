using System.ComponentModel.DataAnnotations;

namespace SymphonyPortal.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Branch name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? City { get; set; }

        public bool IsActive { get; set; } = true;
    }
}