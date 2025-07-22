
using ChamadosCRUD.Data;
using ChamadosCRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamadosCRUD.Controllers
{
    public class LocationController : Controller
    {
        private readonly ChamadosCRUDContext _context;

        public LocationController(ChamadosCRUDContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Location.ToListAsync());
        }
    }
}
