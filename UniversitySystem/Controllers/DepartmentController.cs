using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UniversitySystem.Models;
using UniversitySystemMVC.Data;
using UniversitySystemMVC.Models;


namespace UniversitySystemMVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext context;

        public DepartmentController(AppDbContext _context)
        {
            context = _context; // DbContext جاهز للاستخدام
        }

        // /Department/ShowAll
        public IActionResult ShowAll()
        {
            var departments = context.Departments.ToList();
            return View(departments);
        }

        // /Department/ShowDetails?id=1
        public IActionResult ShowDetails(int id)
        {
            var dept = context.Departments
                .Include(d => d.Students)
                .Include(d => d.Courses)
                .FirstOrDefault(d => d.Id == id);

            if (dept == null)
                return NotFound();

            return View(dept);
        }

        // GET: /Department/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Department/Add
        [HttpPost]
        public IActionResult Add(Department dept)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Add(dept);
                context.SaveChanges();
                return RedirectToAction("ShowAll");
            }

            return View(dept);
        }

        // /Department/ShowDetail?id=1  --> للـ ViewModel اللي طلبتيه
        public IActionResult ShowDetail(int id)
        {
            var department = context.Departments
                .Where(d => d.Id == id)
                .FirstOrDefault();

            if (department == null) return NotFound();

            var studentsOver25 = context.Students
                .Where(s => s.DepartmentId == id && s.Age > 25)
                .ToList();

            var model = new DepartmentDetailsViewModel
            {
                DepartmentName = department.Name,
                DepartmentState = studentsOver25.Count > 50 ? "Main" : "Branch",
                StudentsOver25 = studentsOver25
            };

            return View(model);
        }
    }
}