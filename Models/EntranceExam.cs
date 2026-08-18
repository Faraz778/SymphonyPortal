using System.ComponentModel.DataAnnotations;

namespace SymphonyPortal.Models
{
    public class EntranceExam
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Exam title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Exam date is required")]
        public DateTime ExamDate { get; set; }

        [Required(ErrorMessage = "Last date is required")]
        public DateTime LastDateToApply { get; set; }

        [Required(ErrorMessage = "Exam fees are required")]
        [Range(0, 99999, ErrorMessage = "Enter a valid fee amount")]
        public decimal ExamFees { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}