using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymphonyPortal.Data;
using SymphonyPortal.Models;

namespace SymphonyPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==================
        // LOGIN
        // ==================

        // GET - Login Form
        public IActionResult Login()
        {
            return View();
        }

        // POST - Login Check
        [HttpPost]
        public IActionResult Login(Admin model)
        {
            var admin = _context.Admins
                .FirstOrDefault(a => a.Email == model.Email
                               && a.Password == model.Password);

            if (admin == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }

            // SESSION SAVE
            HttpContext.Session.SetString("AdminId", admin.Id.ToString());
            HttpContext.Session.SetString("AdminName", admin.Name);
            HttpContext.Session.SetString("AdminEmail", admin.Email);

            return RedirectToAction("Dashboard");
        }

      
        public IActionResult Dashboard()
        {
            var adminId = HttpContext.Session.GetString("AdminId");

            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            // STATS
            ViewBag.TotalCourses = _context.Courses.Count();
            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.TotalExams = _context.EntranceExams.Count();
            ViewBag.TotalBranches = _context.Branches.Count();

            // RECENT STUDENTS
            ViewBag.RecentStudents = _context.Students
                .Include(x => x.Course)
                .Include(x => x.Exam)
                .OrderByDescending(x => x.AppliedDate)
                .Take(5)
                .ToList();

            return View();
        }

        // ==================
        // LOGOUT
        // ==================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ==================
        // COURSES
        // ==================

        // LIST - Sab courses dikhao
        public IActionResult Courses()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var courses = _context.Courses
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(courses);
        }

        // CREATE - GET - Form dikhao
        public IActionResult CreateCourse()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            return View();
        }

        // CREATE - POST - Save karo
        [HttpPost]
        public IActionResult CreateCourse(Course model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            _context.Courses.Add(model);
            _context.SaveChanges();

            TempData["success"] = "Course added successfully!";
            return RedirectToAction("Courses");
        }

        // EDIT - GET - Form dikhao
        public IActionResult EditCourse(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var course = _context.Courses.Find(id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        // EDIT - POST - Update karo
        [HttpPost]
        public IActionResult EditCourse(Course model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            var course = _context.Courses.Find(model.Id);

            if (course == null)
                return NotFound();

            // FIELDS UPDATE KARO
            course.Name = model.Name;
            course.Topics = model.Topics;
            course.Fees = model.Fees;
            course.DurationMonths = model.DurationMonths;
            course.IsActive = model.IsActive;

            _context.SaveChanges();

            TempData["success"] = "Course updated successfully!";
            return RedirectToAction("Courses");
        }

        // DELETE - Course hatao
        public IActionResult DeleteCourse(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var course = _context.Courses.Find(id);

            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();

            TempData["success"] = "Course deleted successfully!";
            return RedirectToAction("Courses");
        }

        // ========================
        // ENTRANCE EXAMS
        // ========================

        // LIST
        public IActionResult EntranceExams()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var exams = _context.EntranceExams
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(exams);
        }

        // CREATE - GET
        public IActionResult CreateExam()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            return View();
        }

        // CREATE - POST
        [HttpPost]
        public IActionResult CreateExam(EntranceExam model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            _context.EntranceExams.Add(model);
            _context.SaveChanges();

            TempData["success"] = "Exam added successfully!";
            return RedirectToAction("EntranceExams");
        }

        // EDIT - GET
        public IActionResult EditExam(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var exam = _context.EntranceExams.Find(id);

            if (exam == null)
                return NotFound();

            return View(exam);
        }

        // EDIT - POST
        [HttpPost]
        public IActionResult EditExam(EntranceExam model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            var exam = _context.EntranceExams.Find(model.Id);

            if (exam == null)
                return NotFound();

            // FIELDS UPDATE
            exam.Title = model.Title;
            exam.ExamDate = model.ExamDate;
            exam.LastDateToApply = model.LastDateToApply;
            exam.ExamFees = model.ExamFees;
            exam.Description = model.Description;
            exam.IsActive = model.IsActive;

            _context.SaveChanges();

            TempData["success"] = "Exam updated successfully!";
            return RedirectToAction("EntranceExams");
        }

        // DELETE
        public IActionResult DeleteExam(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var exam = _context.EntranceExams.Find(id);

            if (exam == null)
                return NotFound();

            _context.EntranceExams.Remove(exam);
            _context.SaveChanges();

            TempData["success"] = "Exam deleted successfully!";
            return RedirectToAction("EntranceExams");
        }


         
                    // ========================
                                                // STUDENTS
                    // ========================

        // LIST
        public IActionResult Students()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var students = _context.Students
                .Include(x => x.Exam)
                .Include(x => x.Course)
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(students);
        }

        // DETAIL - ek student ki poori info
        public IActionResult StudentDetail(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var student = _context.Students
                .Include(x => x.Exam)
                .Include(x => x.Course)
                .FirstOrDefault(x => x.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // STATUS UPDATE - Accept ya Reject karo
        public IActionResult UpdateStatus(int id, string status)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var student = _context.Students.Find(id);

            if (student == null)
                return NotFound();

            student.Status = status;

            // ROLL NUMBER ASSIGN - sirf Accept pe
            if (status == "Accepted" &&
                string.IsNullOrEmpty(student.RollNumber))
            {
                student.RollNumber = "SYM-" +
                    DateTime.Now.Year + "-" +
                    student.Id.ToString("D4");
            }

            _context.SaveChanges();

            TempData["success"] = "Student status updated to " + status;
            return RedirectToAction("Students");
        }

        // DELETE
        public IActionResult DeleteStudent(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var student = _context.Students.Find(id);

            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            TempData["success"] = "Student deleted successfully!";
            return RedirectToAction("Students");
        }

        // ========================
        // EXAM RESULTS
        // ========================

        // LIST
        public IActionResult ExamResults()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var results = _context.ExamResults
                .Include(x => x.Student)
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(results);
        }

        // CREATE - GET
        public IActionResult CreateResult()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            // SIRF ACCEPTED STUDENTS KO RESULT DE SAKTE HAIN
            ViewBag.Students = _context.Students
                .Where(x => x.Status == "Accepted")
                .ToList();

            return View();
        }

        // CREATE - POST
        [HttpPost]
        public IActionResult CreateResult(ExamResult model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            // MARKS KE HISAAB SE CLASS ASSIGN KARO
            if (model.MarksObtained >= 50)
            {
                model.ClassAssigned = "Advanced";
                model.CourseFees = 4275;
            }
            else
            {
                model.ClassAssigned = "Basic";
                model.CourseFees = 6000;
            }

            model.ResultDate = DateTime.Now;

            _context.ExamResults.Add(model);
            _context.SaveChanges();

            TempData["success"] = "Result added successfully!";
            return RedirectToAction("ExamResults");
        }

        // EDIT - GET
        public IActionResult EditResult(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var result = _context.ExamResults
                .Include(x => x.Student)
                .FirstOrDefault(x => x.Id == id);

            if (result == null)
                return NotFound();

            ViewBag.Students = _context.Students
                .Where(x => x.Status == "Accepted")
                .ToList();

            return View(result);
        }

        // EDIT - POST
        [HttpPost]
        public IActionResult EditResult(ExamResult model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var result = _context.ExamResults.Find(model.Id);

            if (result == null)
                return NotFound();

            // UPDATE FIELDS
            result.StudentId = model.StudentId;
            result.RollNumber = model.RollNumber;
            result.MarksObtained = model.MarksObtained;
            result.LastDateForPayment = model.LastDateForPayment;
            result.Feedback = model.Feedback;

            // MARKS KE HISAAB SE CLASS UPDATE KARO
            if (result.MarksObtained >= 50)
            {
                result.ClassAssigned = "Advanced";
                result.CourseFees = 4275;
            }
            else
            {
                result.ClassAssigned = "Basic";
                result.CourseFees = 6000;
            }

            _context.SaveChanges();

            TempData["success"] = "Result updated successfully!";
            return RedirectToAction("ExamResults");
        }

        // DELETE
        public IActionResult DeleteResult(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var result = _context.ExamResults.Find(id);

            if (result == null)
                return NotFound();

            _context.ExamResults.Remove(result);
            _context.SaveChanges();

            TempData["success"] = "Result deleted successfully!";
            return RedirectToAction("ExamResults");
        }

        // FAQs
        // LIST
        public IActionResult FAQs()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var faqs = _context.FAQs
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            return View(faqs);
        }

        // CREATE - GET
        public IActionResult CreateFAQ()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            return View();
        }

        // CREATE - POST
        [HttpPost]
        public IActionResult CreateFAQ(FAQ model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            model.CreatedDate = DateTime.Now;

            _context.FAQs.Add(model);
            _context.SaveChanges();

            TempData["success"] = "FAQ added successfully!";
            return RedirectToAction("FAQs");
        }

        // EDIT - GET
        public IActionResult EditFAQ(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var faq = _context.FAQs.Find(id);

            if (faq == null)
                return NotFound();

            return View(faq);
        }

        // EDIT - POST
        [HttpPost]
        public IActionResult EditFAQ(FAQ model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            var faq = _context.FAQs.Find(model.Id);

            if (faq == null)
                return NotFound();

            faq.Question = model.Question;
            faq.Answer = model.Answer;
            faq.DisplayOrder = model.DisplayOrder;
            faq.IsActive = model.IsActive;

            _context.SaveChanges();

            TempData["success"] = "FAQ updated successfully!";
            return RedirectToAction("FAQs");
        }

        // DELETE
        public IActionResult DeleteFAQ(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var faq = _context.FAQs.Find(id);

            if (faq == null)
                return NotFound();

            _context.FAQs.Remove(faq);
            _context.SaveChanges();

            TempData["success"] = "FAQ deleted successfully!";
            return RedirectToAction("FAQs");
        }

        // ========================
        // BRANCHES
        // ========================

        // LIST
        public IActionResult Branches()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var branches = _context.Branches
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(branches);
        }

        // CREATE - GET
        public IActionResult CreateBranch()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            return View();
        }

        // CREATE - POST
        [HttpPost]
        public IActionResult CreateBranch(Branch model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            _context.Branches.Add(model);
            _context.SaveChanges();

            TempData["success"] = "Branch added successfully!";
            return RedirectToAction("Branches");
        }

        // EDIT - GET
        public IActionResult EditBranch(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var branch = _context.Branches.Find(id);

            if (branch == null)
                return NotFound();

            return View(branch);
        }

        // EDIT - POST
        [HttpPost]
        public IActionResult EditBranch(Branch model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            var branch = _context.Branches.Find(model.Id);

            if (branch == null)
                return NotFound();

            branch.Name = model.Name;
            branch.Address = model.Address;
            branch.Phone = model.Phone;
            branch.Email = model.Email;
            branch.City = model.City;
            branch.IsActive = model.IsActive;

            _context.SaveChanges();

            TempData["success"] = "Branch updated successfully!";
            return RedirectToAction("Branches");
        }

        // DELETE
        public IActionResult DeleteBranch(int id)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            var branch = _context.Branches.Find(id);

            if (branch == null)
                return NotFound();

            _context.Branches.Remove(branch);
            _context.SaveChanges();

            TempData["success"] = "Branch deleted successfully!";
            return RedirectToAction("Branches");
        }

        // CREATE STUDENT - GET
        public IActionResult CreateStudent()
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            ViewBag.Exams = _context.EntranceExams
                .Where(x => x.IsActive)
                .ToList();

            ViewBag.Courses = _context.Courses
                .Where(x => x.IsActive)
                .ToList();

            return View();
        }

        // CREATE STUDENT - POST
        [HttpPost]
        public IActionResult CreateStudent(Student model)
        {
            var adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
                return RedirectToAction("Login");

            model.AppliedDate = DateTime.Now;
            model.Status = "Pending";

            _context.Students.Add(model);
            _context.SaveChanges();

            TempData["success"] = "Student added successfully!";
            return RedirectToAction("Students");
        }
    }
}