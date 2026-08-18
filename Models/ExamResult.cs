using System.ComponentModel.DataAnnotations;

namespace SymphonyPortal.Models
{
    public class ExamResult
    {
        public int Id { get; set; }

        // LINKED TO STUDENT
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        [Required(ErrorMessage = "Roll number is required")]
        [StringLength(50, ErrorMessage = "Roll number cannot exceed 50 characters")]
        public string RollNumber { get; set; }

        [Required(ErrorMessage = "Marks are required")]
        [Range(0, 100, ErrorMessage = "Marks must be between 0 and 100")]
        public int MarksObtained { get; set; }

        // CLASS ASSIGNED BASED ON MARKS
        [Required(ErrorMessage = "Class assigned is required")]
        public string ClassAssigned { get; set; }
        // "Basic" → training from scratch - 6 months - $6000
        // "Advanced" → direct certification - 4 months - $4275

        // COURSE FEES BASED ON CLASS
        [Range(0, 99999, ErrorMessage = "Enter a valid fee amount")]
        public decimal CourseFees { get; set; }

        // LAST DATE TO PAY AND JOIN
        public DateTime LastDateForPayment { get; set; }

        // FACULTY FEEDBACK AFTER COURSE COMPLETION
        public string? Feedback { get; set; }

        public DateTime ResultDate { get; set; } = DateTime.Now;
    }
}