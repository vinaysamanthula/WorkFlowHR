using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFlowHR.Data;
using System.Diagnostics;
using WorkFlowHR.Models;

namespace WorkFlowHR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalEmployees =
                await _context.Employees.CountAsync();

            ViewBag.TotalDepartments =
                await _context.Departments.CountAsync();

            var recentEmployees = await _context.Employees
                .Include(e => e.Department)
                .OrderByDescending(e => e.Id)
                .Take(5)
                .ToListAsync();

            return View(recentEmployees);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
