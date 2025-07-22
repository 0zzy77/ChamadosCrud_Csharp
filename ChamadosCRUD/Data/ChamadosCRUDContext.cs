using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChamadosCRUD.Models;

namespace ChamadosCRUD.Data
{
    public class ChamadosCRUDContext : DbContext
    {
        public ChamadosCRUDContext (DbContextOptions<ChamadosCRUDContext> options)
            : base(options)
        {

        }

        public DbSet<ChamadosCRUD.Models.Location> Location { get; set; } = default!;
        public DbSet<ChamadosCRUD.Models.Role> Role { get; set; } = default!;
        public DbSet<ChamadosCRUD.Models.StatusTicket> StatusTickets { get; set; } = default!;

    }
}
