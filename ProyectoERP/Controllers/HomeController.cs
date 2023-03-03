using Microsoft.AspNetCore.Mvc;

namespace ProyectoERP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
