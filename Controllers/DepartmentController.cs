
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFlowHR.Data;
using WorkFlowHR.Models;

namespace WorkFlowHR.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Department/Index
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .OrderBy(d => d.Name)
                .ToListAsync();

            return View(departments);
        }

        // GET: Department/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Department/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _context.Departments
                .FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(department);
            }

            var existingDepartment = await _context.Departments
                .FindAsync(id);

            if (existingDepartment == null)
            {
                return NotFound();
            }

            existingDepartment.Name = department.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Department/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments
                .FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            ViewBag.HasEmployees = await _context.Employees
                .AnyAsync(e => e.DepartmentId == id);

            return View(department);
        }

        // POST: Department/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments
                .FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            bool hasEmployees = await _context.Employees
                .AnyAsync(e => e.DepartmentId == id);

            if (hasEmployees)
            {
                TempData["ErrorMessage"] =
                    "Cannot delete this department because employees are assigned to it.";

                return RedirectToAction(nameof(Index));
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}