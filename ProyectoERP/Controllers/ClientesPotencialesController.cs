using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Helpers;
using ProyectoERP.Models;
using ProyectoERP.Repositories;

namespace ProyectoERP.Controllers
{
    public class ClientesPotencialesController : Controller
    {
        private IRepo repo;
        private HelperMail helperMail;
        //private HelperPathProvider helperPath;
        public ClientesPotencialesController(IRepo repo, HelperMail helperMail)
        {
            this.repo = repo;
            this.helperMail = helperMail;
            //this.helperPath = helperPath;
        }
        public IActionResult Index(int? idinteresado, string? correos)
        {
            List<Curso> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            List<ClientePotencial> clientes = this.repo.GetClientesP();
            if (idinteresado != null)
            {
                ClientePotencial cliente = this.repo.GetCliente(idinteresado.Value);
                ViewBag.CLIENTE = cliente;
            }
            if (correos != null)
            {
                string[] correosArray = correos.Split(',');
                ViewBag.CORREOS = correosArray;
            }
            return View(clientes);
        }
        //public IActionResult MailMasivo(string? correos)
        //{
        //    List<Curso> cursos = this.repo.GetCursos();
        //    ViewBag.CURSOS = cursos;
        //    List<ClientePotencial> clientes = this.repo.GetClientesP();
        //    if (correos != null)
        //    {
        //        string[] correosArray = correos.Split(',');
        //        ViewBag.CORREOS = correosArray;
        //    }
        //    return RedirectToAction("Index", clientes);
        //}

        [HttpPost]
        public IActionResult Index(string curso)
        {
            List<Curso> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            List<ClientePotencial> clientes = this.repo.FindClientesP(curso);
            return View(clientes);
        }
        public IActionResult InsertarCliente()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> InsertarCliente(string nombrecliente, string tlf, string email, string? comentarios, string codcurso)
        {
            await this.repo.InsertClienteP(nombrecliente, tlf, email, comentarios, codcurso);
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
