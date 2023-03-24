using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Models;
using ProyectoERP.Repositories;

namespace ProyectoERP.Controllers
{
    public class AlumnosController : Controller
    {
        private IRepo repo;
        //private HelperPathProvider helperPath;
        public AlumnosController(IRepo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<AlumnoPagos> alumnos = await this.repo.GetAlumnosPagos();
            return View(alumnos);
        }
        public async Task<IActionResult> _AlumnosPagos(string nombrealumno)
        {
            List<AlumnoPagos> alumnos = await this.repo.FiltroNombreAlumnoAsync(nombrealumno);
            ViewBag.Cursos = this.repo.GetCursos();
            return PartialView("_AlumnosPagos", alumnos);
        }
    }
}
