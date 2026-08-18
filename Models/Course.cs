using System.ComponentModel.DataAnnotations;

namespace SymphonyPortal.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Topics are required")]
        public string Topics { get; set; }

        [Required(ErrorMessage = "Fees are required")]
        [Range(0, 99999, ErrorMessage = "Enter a valid fee amount")]
        public decimal Fees { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 24, ErrorMessage = "Duration must be between 1 and 24 months")]
        public int DurationMonths { get; set; }

        public bool IsActive { get; set; } = true;
    }
}