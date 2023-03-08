using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Models;
using ProyectoERP.Repositories;

namespace ProyectoERP.Controllers
{
    public class ClientesPotencialesController : Controller
    {
        private IRepo repo;
        public ClientesPotencialesController(IRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<string> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            List<ClientePotencial> clientes = this.repo.GetClientesP();
            return View(clientes);
        }

        [HttpPost]
        public IActionResult Index(string curso)
        {
            List<string> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            List<ClientePotencial> clientes = this.repo.FindClientesP(curso);
            return View(clientes);
        }
    }
}
