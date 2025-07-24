using ChamadosCRUD.Data;
using ChamadosCRUD.Models;
using ChamadosCRUD.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_context.Role, "Id", "Name");//Dúvida --> quando utilizar viewBag e quando utilizar as listagens da ViewModel
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Email, RoleId, Password")] UserViewModel userData)
        {

            if (ModelState.IsValid)//verifica se os dados enviados estão válidos de acordo com as validações inseridos na model
            {
                
                var hasher = new PasswordHasher<User>();

                var user = new User
                {
                    Name = userData.Name,
                    Email = userData.Email,
                    RoleId = userData.RoleId,
                    PasswordHash = hasher.HashPassword(null, userData.Password),
                };

                _context.Add(user);//equivalente ao INSERT
                await _context.SaveChangesAsync();//executa o INSERT acima
                return RedirectToAction(nameof(Index));//é o mesmo que escrever "Index" mas com segurança de compilação (se você renomear o método Index, o código ainda funciona)
            }//RedirecToAction --> faz com que o navegador seja redirecionado, evitando o reenvio do formulário se o usuário der F5
            
            //debug model
            foreach(var entry in ModelState)
            {
                foreach(var error in entry.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Campo: {entry.Key} - Erro: {error.ErrorMessage}");
                }
            }

            ViewBag.Roles = new SelectList(_context.Role, "Id", "Name");
            return View(userData);//caso não entre no if por conta de dados inválidos, irá retornar com as mensagens de erro
        }

       
    }
}
