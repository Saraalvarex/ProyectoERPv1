using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Models;
using ProyectoERP.Repositories;
using System.Text.RegularExpressions;

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
        public async Task<IActionResult> FiltroGrupo(string codgrupo)
        {
            Grupo grupo = await this.repo.FiltroGruposCod(codgrupo);
            ViewBag.Cursos = this.repo.GetCursos();
            ViewBag.GRUPO = grupo;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> FiltroCurso(string curso)
        {
            List<Grupo> grupos = await this.repo.FiltroGruposCurso(curso);
            ViewBag.Cursos = this.repo.GetCursos();
            return RedirectToAction("Index", grupos);
        }

        [HttpPost]
        public async Task<IActionResult> FiltroFecha(DateTime fechainicio)
        {
            List<Grupo> grupos = await this.repo.FiltroGruposFecha(fechainicio);
            ViewBag.Cursos = this.repo.GetCursos();
            return RedirectToAction("Index", grupos);
        }
    }
}
