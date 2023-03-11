using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Models;
using ProyectoERP.Repositories;

namespace ProyectoERP.Controllers
{
    public class InsertarClienteController : Controller
    {
        private IRepo repo;
        public InsertarClienteController(IRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertarCliente(string nombrecliente, string tlf, string email, string? comentarios, string codcurso)
        {
            this.repo.InsertClienteP(nombrecliente, tlf, email, comentarios, codcurso);
            //List<string> cursos = this.repo.GetCursos();
            //ViewBag.CURSOS = cursos;
            //List<ClientePotencial> clientes = this.repo.GetClientesP();
            return View();
        }
    }
}
