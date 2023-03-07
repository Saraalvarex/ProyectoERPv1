using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Models;
using ProyectoERP.Repositories;

#region VISTAS
//CREATE VIEW V_INTERESADOS_CURSO
//AS
//SELECT I.IDINTERESADO, I.NOMBRE, I.TLF, I.EMAIL, I.COMENTARIOS, C.NOMBRE AS CURSO
//FROM INTERESADOS I
//INNER JOIN CURSOS C ON I.CODCURSO = C.CODCURSO
//GO

//SELECT * FROM V_INTERESADOS_CURSO
#endregion
namespace ProyectoERP.Controllers
{
    public class ClientesPotencialesController : Controller
    {
        private IRepo repo;
        public ClientesPotencialesController(IRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<ClientePotencial> clientes = this.repo.GetClientesP();
            return View(clientes);
        }
    }
}
