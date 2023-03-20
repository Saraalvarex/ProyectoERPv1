using Microsoft.AspNetCore.Mvc;

namespace ProyectoERP.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
