using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProyectoERP.Models;
using ProyectoERP.Repositories;

namespace ProyectoERP.Controllers
{
    public class ManagedController : Controller
    {
        private IRepo repo;
        public ManagedController(IRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string nombreusuario, string clave)
        {
            Usuario usuario = await this.repo.ExisteUsuario(nombreusuario, clave);
            if (usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimname = new Claim(ClaimTypes.Name, nombreusuario);
                Claim claimid = new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                Claim claimrol = new Claim(ClaimTypes.Role, usuario.Rol.ToString());
                identity.AddClaim(claimname);
                identity.AddClaim(claimid);
                identity.AddClaim(claimrol);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewBag.MENSAJE = "Usuario/Password Incorrectos";
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ErrorAcceso()
        {
            return View();
        }
    }
}
