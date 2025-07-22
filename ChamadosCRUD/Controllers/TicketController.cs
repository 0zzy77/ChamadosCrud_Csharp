using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamadosCRUD.Controllers
{
    public class TicketController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
