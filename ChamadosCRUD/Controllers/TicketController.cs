﻿using ChamadosCRUD.Data;
using ChamadosCRUD.Models;
using ChamadosCRUD.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;


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

        public async Task<IActionResult> Edit(int id)
        {
            
            if (id == null)
                return NotFound();

            Ticket ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound();

            var status = await _context.StatusTickets.ToListAsync();
            var users = await _context.Users.ToListAsync();

            TicketEditViewModel ticketVM = new TicketEditViewModel
            {
                Id = ticket.Id,
                StatusId = ticket.StatusTicketId,
                AssignedToId = ticket.AssignedToId,
                Status = status.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name,
                    Selected = s.Id == ticket.StatusTicketId
                }).ToList(),
                Users = users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name,
                    Selected = u.Id == ticket.AssignedToId
                }).ToList()

            };

            return View(ticketVM);
        }

        public async Task<IActionResult> AssignTo(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Ticket ticket = await _context.Tickets.FindAsync(id);

            if(ticket == null)
            {
                return NotFound();
            }

            ticket.AssignedToId = Convert.ToInt32(userId);
            ticket.UpdatedAt = DateTime.Now.ToUniversalTime();

            try
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();

                TempData["Assigned"] = "Chamado atribuído com sucesso.";

                //debug model
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Campo: {entry.Key} - Erro: {error.ErrorMessage}");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                TempData["AssignedError"] = "Ocorreu um erro ao tentar atribuir este chamado.";
                return View();
            }

        }

        public async Task<IActionResult> UnassignTo(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Ticket ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            ticket.AssignedToId = null;
            ticket.UpdatedAt = DateTime.Now.ToUniversalTime();

            try
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();

                TempData["Assigned"] = "Chamado desatribuído com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["AssignedError"] = "Ocorreu um erro ao tentar desatribuir este chamado.";
                return View();
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            //Ticket ticket = await _context.Tickets.FindAsync(id); //pega o objeto porém não pega as chaves estrangeiras

            Ticket ticket = await _context.Tickets
                .Include(t => t.Location)
                .Include(t => t.Status)
                .Include(t => t.AssignedTo)
                .FirstOrDefaultAsync(t => t.Id == id);


            if(ticket == null)
            {
                return NotFound();
            }

            return View(ticket);

        }


    }
}
