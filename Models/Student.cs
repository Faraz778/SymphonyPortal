using System.ComponentModel.DataAnnotations;

namespace SymphonyPortal.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(15, ErrorMessage = "Enter a valid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        // WHICH EXAM APPLIED FOR
        public int ExamId { get; set; }
        public EntranceExam? Exam { get; set; }

        // WHICH COURSE CHOSEN
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        // PAYMENT DETAILS
        [Required(ErrorMessage = "Payment method is required")]
        public string PaymentMethod { get; set; }

        public string? ReceiptNumber { get; set; }

        public string? ChequeOrDDNumber { get; set; }

        public string? BankDetails { get; set; }

        // ROLL NUMBER — assigned after payment accepted
        public string? RollNumber { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime AppliedDate { get; set; } = DateTime.Now;
    }
}