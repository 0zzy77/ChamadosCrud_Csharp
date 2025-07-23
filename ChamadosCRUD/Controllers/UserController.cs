using ChamadosCRUD.Data;
using ChamadosCRUD.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChamadosCRUD.Controllers
{
    public class UserController : Controller
    {
        private readonly ChamadosCRUDContext _context;

        public UserController(ChamadosCRUDContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users //-> acessa o banco de dados através do DbContext
                .Include(u => u.Role)//-> informa ao Entity Framework que você quer "incluir" a entidade relacionada Role (gera um join interno)
                .Select(u => new UserViewModel //-> converte os dados brutos em viewModel(permite exibir somente o necessário
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    RoleName = u.Role.Name //join

                }).ToList();

            return View(users);
        }
    }
}
