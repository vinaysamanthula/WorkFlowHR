using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkFlowHR.Models;

namespace WorkFlowHR.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            ViewBag.Message = "Welcome to WorkFlow HR";
            return View();
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
