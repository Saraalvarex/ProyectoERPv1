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
        public IActionResult Index(int? idinteresado)
        {
            List<string> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            List<ClientePotencial> clientes = this.repo.GetClientesP();
            if (idinteresado != null)
            {
                ClientePotencial cliente = this.repo.GetCliente(idinteresado.Value);
                ViewBag.CLIENTE = cliente;
            }
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

        public IActionResult _InsertarCliente()
        {
            return View("_InsertarCliente", new ClientePotencial());
        }
    }
}
