using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Helpers;
using ProyectoERP.Models;
using ProyectoERP.Repositories;
using static NuGet.Packaging.PackagingConstants;

namespace ProyectoERP.Controllers
{
    public class ClientesPotencialesController : Controller
    {
        private IRepo repo;
        private HelperMail helperMail;
        public ClientesPotencialesController(IRepo repo, HelperMail helperMail)
        {
            this.repo = repo;
            this.helperMail = helperMail;
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
        public IActionResult InsertarCliente()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult InsertarCliente(string nombrecliente, string tlf, string email, string? comentarios, string codcurso)
        {
            this.repo.InsertClienteP(nombrecliente, tlf, email, comentarios, codcurso);
            return RedirectToAction("Index");
        }

        //Enviar correo info interesados
        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string para, string asunto, string mensaje)
        {
            await this.helperMail.SendMailAsync(para, asunto, mensaje);
            ViewBag.MENSAJE = "Email enviado correctamente";
            return RedirectToAction("Index");
        }
    }
}
