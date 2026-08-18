using System.ComponentModel.DataAnnotations;

namespace SymphonyPortal.Models
{
    public class FAQ
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Question is required")]
        [StringLength(500, ErrorMessage = "Question cannot exceed 500 characters")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Answer is required")]
        public string Answer { get; set; }

        [Range(1, 999, ErrorMessage = "Enter a valid order number")]
        public int DisplayOrder { get; set; } = 1;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}