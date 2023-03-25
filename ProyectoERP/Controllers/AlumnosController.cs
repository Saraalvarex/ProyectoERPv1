using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ProyectoERP.Helpers;
using ProyectoERP.Models;
using ProyectoERP.Repositories;
using OfficeOpenXml;

namespace ProyectoERP.Controllers
{
    public class AlumnosController : Controller
    {
        private IRepo repo;
        private HelperPathProvider helperPath;
        public AlumnosController(IRepo repo, HelperPathProvider helperPath)
        {
            this.repo = repo;
            this.helperPath = helperPath;
        }
        public async Task<IActionResult> GenerarFactura(string dni, int idalumno, string nombrealumno, string direccion, int pago, string concepto, DateTime? fecha)
        {
            if (fecha == null)
            {
                fecha= DateTime.Now;
            }
            //Me pide licencia comercial
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            //Ruta de excell en el que escribir
            string rutaArchivoInicial = helperPath.MapPath("FACTURA.xlsx", Folders.Facturas);
            var file = new FileInfo(rutaArchivoInicial);
            var package = new ExcelPackage(file);
            //Aqui cojo la hoja de dentro del excell
            var sheet = package.Workbook.Worksheets["Hoja1"];
            //Agregar los datos de la factura
            sheet.Cells["B18"].Value = nombrealumno;
            sheet.Cells["B19"].Value = direccion;
            sheet.Cells["B22"].Value = concepto;
            sheet.Cells["G17"].Value = fecha.Value;
            sheet.Cells["G18"].Value = dni;
            sheet.Cells["G22"].Value = pago;
            //Guardamos el archivo de Excel en la ruta
            string nombreSinEspacios = nombrealumno.Replace(" ", ""); //quitamos todos los espacios en blanco
            string rutaArchivoFinal = helperPath.MapPath(nombreSinEspacios + ".xlsx", Folders.Facturas);
            var fileModificado = new FileInfo(rutaArchivoFinal);
            package.SaveAs(fileModificado);
            int codfactura = 0;
            this.repo.InsertFact(idalumno, nombreSinEspacios + ".xlsx", ref codfactura);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            List<Curso> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            List<AlumnoPagos> alumnos = await this.repo.GetAlumnosPagos();
            return View(alumnos);
        }

        //[HttpPost]
        public async Task<IActionResult> GetAlumno(int idalumno)
        {
            List<Curso> cursos = this.repo.GetCursos();
            AlumnoPagos alumno = await this.repo.GetAlumno(idalumno);
            ViewBag.ALUMNO = alumno;
            ViewBag.CURSOS = cursos;
            List<AlumnoPagos> alumnos = await this.repo.GetAlumnosPagos();
            return RedirectToAction("Index",alumnos);
        }
        public async Task<IActionResult> _Factura(int idalumno)
        {
            List<Curso> cursos = this.repo.GetCursos();
            AlumnoPagos alumno = await this.repo.GetAlumno(idalumno);
            ViewBag.ALUMNO = alumno;
            ViewBag.CURSOS = cursos;
            List<AlumnoPagos> alumnos = await this.repo.GetAlumnosPagos();
            return PartialView("_Factura", alumnos);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? nombrealumno, DateTime? fecha)
        {
            List<AlumnoPagos> alumnos = new List<AlumnoPagos>();
            List<Curso> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            //alumnos = await this.repo.GetAlumnosPagos();
            if (nombrealumno != null)
            {
                alumnos = await this.repo.FiltroNombreAlumnoAsync(nombrealumno);
            }
            else if (fecha != null)
            {
                alumnos = await this.repo.FiltroAlumnosPagosFecha(fecha.Value);
            }
            return View("Index", alumnos);
        }

        public async Task<IActionResult> _AlumnosPagos(string? nombrealumno, DateTime? fecha)
        {
            List<Curso> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            List<AlumnoPagos> alumnos = new List<AlumnoPagos>();
            //alumnos = await this.repo.GetAlumnosPagos();
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
