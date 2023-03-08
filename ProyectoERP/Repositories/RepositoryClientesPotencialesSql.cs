using ProyectoERP.Data;
using ProyectoERP.Models;
#region VISTAS
//CREATE VIEW V_INTERESADOS_CURSO
//AS
//SELECT I.IDINTERESADO, I.NOMBRE, I.TLF, I.EMAIL, I.COMENTARIOS, C.NOMBRE AS CURSO
//FROM INTERESADOS I
//INNER JOIN CURSOS C ON I.CODCURSO = C.CODCURSO
//GO

//SELECT * FROM V_INTERESADOS_CURSO
#endregion
namespace ProyectoERP.Repositories
{
    public class RepositoryClientesPotencialesSql : IRepo
    {
        private ErpContext context;
        public RepositoryClientesPotencialesSql(ErpContext context)
        {
            this.context = context;
        }
        public void DeleteClienteP(int id)
        {
            throw new NotImplementedException();
        }

        public List<ClientePotencial> FindClientesP(string curso)
        {
            var consulta = from datos in this.context.ClientesPotenciales
                           where datos.Curso == curso
                           select datos;
            return consulta.ToList();
        }

        public ClientePotencial GetCliente(int id)
        {
            throw new NotImplementedException();
        }

        public List<ClientePotencial> GetClientesP()
        {
            var consulta = from datos in this.context.ClientesPotenciales
                           select datos;
            return consulta.ToList();
        }

        public List<string> GetCursos()
        {
            var cursos = from datos in this.context.Cursos
                         select datos.NombreCurso;
            return cursos.ToList();
        }

        public void InsertClienteP(int id, string nombre, string tlf, string email, string comentarios)
        {
            throw new NotImplementedException();
        }

        public void UpdateClienteP(int id, string nombre, string tlf, string email, string comentarios)
        {
            throw new NotImplementedException();
        }
    }
}
