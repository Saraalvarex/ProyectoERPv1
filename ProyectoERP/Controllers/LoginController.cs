using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Models;
using ProyectoERP.Repositories;

namespace ProyectoERP.Controllers
{
    public class LoginController : Controller
    {
        private IRepo repo;

        public LoginController(IRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string clave)
        {
            Usuario user = this.repo.LoginUser(username, clave);
            if (user == null)
            {
                ViewBag.MENSAJE = "Credenciales incorrectas";
                return View();
            }
            else
            {
                return View(user);
            }
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
