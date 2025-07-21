using Microsoft.AspNetCore.Mvc;

namespace ChamadosCRUD.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
