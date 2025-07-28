using ChamadosCRUD.Data;
using ChamadosCRUD.Models;
using ChamadosCRUD.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Security.Claims;

namespace ChamadosCRUD.Controllers
{
    public class UserController : Controller
    {
        private readonly ChamadosCRUDContext _context;

        public UserController(ChamadosCRUDContext context)
        {
            _context = context;
        }

        [Authorize]
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

        [Authorize]
        public IActionResult Create()
        {
            var userRequesterId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Role));

            if (userRequesterId > 2)
            {
                TempData["Unauthorized"] = "Você não possui permissão para esta ação.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = new SelectList(_context.Role, "Id", "Name");//Dúvida --> quando utilizar viewBag e quando utilizar as listagens da ViewModel
            return View();

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Email, RoleId, Password")] UserViewModel userData)
        {
            var userRequesterId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Role));

            if (userRequesterId > 2)
            {
                TempData["Unauthorized"] = "Você não possui permissão para esta ação.";
                return RedirectToAction(nameof(Index));
            }

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

                TempData["Success"] = $"Usuário {user.Name} criado com sucesso.";

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

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            var roles = await _context.Role.ToListAsync();

            UserEditViewModel userVM = new UserEditViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleId = user.RoleId,
                Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = r.Id == user.RoleId
                }).ToList()
            };

            return View(userVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Name, Email, RoleId")] UserEditViewModel userVM)
        {

            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Campo: {entry.Key} - Erro: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = userVM.Name;
            user.Email = userVM.Email;
            user.RoleId = userVM.RoleId;

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException e)
            {
                return View(userVM);

            }



            
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userRequesterId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Role));

            var userToDelete = await _context.Users.FindAsync(id);

            if (userRequesterId > 2 || userRequesterId == id)
            {
                TempData["Unauthorized"] = "Você não possui permissão para esta ação.";
                return RedirectToAction(nameof(Index));
            }

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);

                TempData["DeletedSuccess"] = "Deletado com sucesso.";
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
