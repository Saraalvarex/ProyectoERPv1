using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ProyectoERP.Helpers;
using ProyectoERP.Models;
using ProyectoERP.Repositories;
using System.Diagnostics;

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
        public async Task<IActionResult> Factura(int idalumno)
        {
            List<Curso> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            Alumno alumno = await this.repo.GetAlumno(idalumno);
            return View(alumno);
        }
        public async Task<IActionResult> GenerarFactura(string dni, int idalumno, string nombrealumno, string direccion, int pago, string concepto, DateTime? fecha, string? curso)
        {
            if (fecha == null)
            {
                fecha = DateTime.Now;
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
            sheet.Cells["C22"].Value = concepto;
            sheet.Cells["D22"].Value = curso;
            sheet.Cells["G22"].Value = pago;
            sheet.Cells["G17"].Value = fecha.Value;
            sheet.Cells["G18"].Value = dni;

            //Guardamos el archivo de Excel en la ruta
            string nombreSinEspacios = nombrealumno.Replace(" ", ""); //quitamos todos los espacios en blanco
            int codfactura = await this.repo.InsertFactAsync(idalumno, "\\" + nombreSinEspacios);
            string rutaArchivoFinal = helperPath.MapPath(nombreSinEspacios+codfactura+".pdf", Folders.Facturas);

            var fileModificado = new FileInfo(rutaArchivoFinal);
            package.SaveAs(fileModificado);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = rutaArchivoFinal,
                UseShellExecute = true
            };
            Process.Start(startInfo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string? codgrupo)
        {
            List<Curso> cursos = this.repo.GetCursos();
            ViewBag.CURSOS = cursos;
            List<AlumnoPagos> alumnos = new List<AlumnoPagos>();
            if (codgrupo != null)
            {
                alumnos = await this.repo.GetAlumnosGrupoAsync(codgrupo);
            }else
            {
                alumnos = await this.repo.GetAlumnosPagos();
            }
            return View(alumnos);
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
    }
}
