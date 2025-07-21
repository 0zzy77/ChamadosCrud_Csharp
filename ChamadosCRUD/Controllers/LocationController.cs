using Microsoft.AspNetCore.Mvc;

namespace ChamadosCRUD.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
