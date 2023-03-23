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
        private RepositoryERPSql repo;
        public ManagedController(RepositoryERPSql repo)
        {
            this.repo = repo;
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string username, string password)
        {
            Usuario usuario = await this.repo.ExisteUsuario(username, int.Parse(password));
            if (usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimname = new Claim(ClaimTypes.Name, username);
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
