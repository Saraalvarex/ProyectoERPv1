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

        public async Task<IActionResult> _AlumnosPagos(string? nombrealumno, DateTime? fecha)
        {
            List<AlumnoPagos> alumnos = new List<AlumnoPagos>();
            if (nombrealumno != null)
            {
                alumnos = await this.repo.FiltroNombreAlumnoAsync(nombrealumno);
            }
            else if (fecha != null)
            {
                alumnos = await this.repo.FiltroAlumnosPagosFecha(fecha.Value);
            }
            return PartialView("_AlumnosPagos", alumnos);
        }
    }
}
