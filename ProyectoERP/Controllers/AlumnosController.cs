using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Repositories;

namespace ProyectoERP.Controllers
{
    public class AlumnosController : Controller
    {
        private IRepo repo;
        //private HelperPathProvider helperPath;
        public AlumnosController(IRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
