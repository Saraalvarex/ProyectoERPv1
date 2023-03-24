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

        public async Task<IActionResult> _Grupo(string codgrupo)
        {
            Grupo grupo = await this.repo.FiltroGruposCod(codgrupo);
            ViewBag.Cursos = this.repo.GetCursos();
            ViewBag.GRUPO = grupo;
            return PartialView("_Grupo", grupo);
        }

        public async Task<IActionResult> _Grupos(string? curso, DateTime? fechainicio)
        {
            List<Grupo> grupos = new List<Grupo>();
            if (curso != null)
            {
                grupos = await this.repo.FiltroGruposCurso(curso);
                ViewBag.Cursos = this.repo.GetCursos();
            }else if(fechainicio != null)
            {
                grupos = await this.repo.FiltroGruposFecha(fechainicio.Value);
                ViewBag.Cursos = this.repo.GetCursos();
            }
            return PartialView("_Grupos", grupos);
        }
    }
}
