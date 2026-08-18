using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymphonyPortal.Data;
using SymphonyPortal.Models;

namespace SymphonyPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // HOME
        public IActionResult Index()
        {
            // STATS
            ViewBag.TotalCourses = _context.Courses.Count();
            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.TotalExams = _context.EntranceExams.Count();
            ViewBag.TotalBranches = _context.Branches.Count();

            // COURSES - sirf active
            ViewBag.Courses = _context.Courses
                .Where(x => x.IsActive)
                .Take(6)
                .ToList();

            // EXAMS - sirf active
            ViewBag.Exams = _context.EntranceExams
                .Where(x => x.IsActive)
                .Take(4)
                .ToList();

            return View();
        }

        // COURSES
        public IActionResult Courses()
        {
            var courses = _context.Courses
                .Where(x => x.IsActive)
                .ToList();

            return View(courses);
        }

        // EXAMS
        public IActionResult Exams()
        {
            var exams = _context.EntranceExams
                .Where(x => x.IsActive)
                .ToList();

            return View(exams);
        }

        // RESULTS
        public IActionResult Results()
        {
            return View();
        }

        // RESULTS CHECK - POST
        [HttpPost]
        public IActionResult Results(string rollNumber)
        {
            if (string.IsNullOrEmpty(rollNumber))
            {
                ViewBag.Error = "Please enter roll number";
                return View();
            }

            var result = _context.ExamResults
                .Include(x => x.Student)
                .FirstOrDefault(x => x.RollNumber == rollNumber);

            if (result == null)
            {
                ViewBag.Error = "No result found for this roll number";
                return View();
            }

            return View(result);
        }

        // FAQ
        public IActionResult FAQ()
        {
            var faqs = _context.FAQs
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            return View(faqs);
        }

        // CONTACT
        public IActionResult Contact()
        {
            var branches = _context.Branches
                .Where(x => x.IsActive)
                .ToList();

            return View(branches);
        }

        // ABOUT
        public IActionResult About()
        {
            return View();
        }

        // APPLY
        public IActionResult Apply()
        {
            ViewBag.Exams = _context.EntranceExams
                .Where(x => x.IsActive)
                .ToList();

            ViewBag.Courses = _context.Courses
                .Where(x => x.IsActive)
                .ToList();

            return View();
        }

        // APPLY - POST
        [HttpPost]
        public IActionResult Apply(Student model)
        {
            model.AppliedDate = DateTime.Now;
            model.Status = "Pending";

            _context.Students.Add(model);
            _context.SaveChanges();

            TempData["success"] = "Application submitted successfully! We will contact you soon.";
            return RedirectToAction("Apply");
        }
    }
}