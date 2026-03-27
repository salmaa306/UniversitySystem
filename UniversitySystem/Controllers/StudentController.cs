using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Models;
using UniversitySystemMVC.Data;


namespace UniversitySystemMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // ===========================
        // List all students
        // ===========================
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Include(s => s.Department) // join with Department
                .ToListAsync();
            return View(students);
        }

        // ===========================
        // Student details
        // ===========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null) return NotFound();

            return View(student);
        }

        // ===========================
        // Create student GET
        // ===========================
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList(); // for dropdown
            return View();
        }

        // ===========================
        // Create student POST
        // ===========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = _context.Departments.ToList();
            return View(student);
        }

        // ===========================
        // Edit student GET
        // ===========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            ViewBag.Departments = _context.Departments.ToList();
            return View(student);
        }

        // ===========================
        // Edit student POST
        // ===========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = _context.Departments.ToList();
            return View(student);
        }

        // ===========================
        // Delete student GET
        // ===========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null) return NotFound();

            return View(student);
        }

        // ===========================
        // Delete student POST
        // ===========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ===========================
        // Helper to check if student exists
        // ===========================
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}