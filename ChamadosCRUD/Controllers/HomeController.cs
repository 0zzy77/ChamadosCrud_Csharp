using ChamadosCRUD.Data;
using ChamadosCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChamadosCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private readonly ChamadosCRUDContext _context;

        public HomeController(ChamadosCRUDContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Roles = new SelectList(_context.Role, "Id", "Name");

            ViewBag.Location = new SelectList(_context.Location, "Id", "Name");
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
