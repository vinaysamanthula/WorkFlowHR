using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkFlowHR.Data;
using WorkFlowHR.Models;


namespace WorkFlowHR.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(
                await _context.Departments.ToListAsync(),
                "Id",
                "Name"
                );
            return View();
        }

        [HttpPost]
        // POST: Employee/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(
                    await _context.Departments.ToListAsync(),
                    "Id",
                    "Name",
                    employee.DepartmentId
                    );
                return View(employee);
            }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Temporary plaeholder for redirect
      
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
            return View(employees);
        }
        // GET: Employee/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Departments = new SelectList(
                await _context.Departments.ToListAsync(),
                "Id",
                "Name",
                employee.DepartmentId
            );

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .Select(x => new
                    {
                        Field = x.Key,
                        Errors = x.Value!.Errors
                            .Select(e => e.ErrorMessage)
                            .ToList()
                    })
                    .ToList();

                foreach (var error in errors)
                {
                    Console.WriteLine(
                        $"{error.Field}: {string.Join(", ", error.Errors)}");
                }

                ViewBag.Departments = new SelectList(
                    await _context.Departments.ToListAsync(),
                    "Id",
                    "Name",
                    employee.DepartmentId
                );

                return View(employee);
            }

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        // GET: Employee/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }   


    }
}
