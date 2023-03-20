using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Models;
using ProyectoERP.Repositories;

namespace ProyectoERP.Controllers
{
    public class GruposController : Controller
    {
        private IRepo repo;
        //private HelperPathProvider helperPath;
        public GruposController(IRepo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Grupo> grupos = await this.repo.GetGrupos();
            ViewBag.Cursos = this.repo.GetCursos();
            return View(grupos);
        }

        [HttpPost]
        public async Task<IActionResult> FiltroGrupo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FiltroCurso()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FiltroFecha()
        {
            return View();
        }
    }
}
