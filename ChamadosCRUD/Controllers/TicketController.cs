using ChamadosCRUD.Data;
using ChamadosCRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

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
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Location)
                .Include(t => t.Status)
                .Include(t => t.AssignedTo)
                .ToListAsync();

            return View(tickets);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Locations = new SelectList(_context.Location, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, LocationId, Description, RequesterName, RequesterEmail, RequesterPhone")]Ticket model)
        {
            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    Title = model.Title,
                    Description = model.Description,
                    RequesterName = model.RequesterName,
                    RequesterEmail = model.RequesterEmail,
                    RequesterPhone = model.RequesterPhone,
                    LocationId = model.LocationId
                };

                _context.Add(ticket);
                await _context.SaveChangesAsync();

                TempData["Success"] = $"Chamado sob o protocolo {ticket.Id} enviado com sucesso.";
                return RedirectToAction("Create", "Ticket");
            }

            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Campo: {entry.Key} - Erro: {error.ErrorMessage}");
                }
            }

            ViewBag.Locations = new SelectList(_context.Location, "Id", "Name");
            
            return View(model);
        }

        
    }
}
