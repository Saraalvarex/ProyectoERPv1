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
            List<Curso> cursos = this.repo.GetCursos();
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
            List<Curso> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            List<ClientePotencial> clientes = this.repo.FindClientesP(curso);
            return View(clientes);
        }
        //Es obligatorio poner este iaction?
        //public IActionResult InsertarCliente()
        //{
        //    return View("Index");
        //}

        [HttpPost]
        public IActionResult InsertarCliente(string nombrecliente, string tlf, string email, string? comentarios, string codcurso)
        {
            this.repo.InsertClienteP(nombrecliente, tlf, email, comentarios, codcurso);
            return RedirectToAction("Index");
        }

        //Enviar correo
    }
}
