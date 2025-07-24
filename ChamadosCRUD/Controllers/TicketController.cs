using ChamadosCRUD.Data;
using ChamadosCRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChamadosCRUD.Controllers
{
    public class TicketController : Controller
    {
        private readonly ChamadosCRUDContext _context;
        public TicketController(ChamadosCRUDContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, LocationId, Description, RequesterName, RequesterEmail, RequesterPhone")]Ticket model)
        {
            if (ModelState.IsValid)
            {

            }

            ViewBag.Locations = new SelectList(_context.Location, "Id", "Name");
            return View("Views/Home/index.cshtml");
        }
    }
}
