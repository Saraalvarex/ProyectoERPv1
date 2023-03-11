using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoERP.Data;
using ProyectoERP.Models;
#region VISTAS
//CREATE VIEW V_INTERESADOS_CURSOS
//AS
//SELECT I.IDINTERESADO, I.NOMBRE, I.TLF, I.EMAIL, I.COMENTARIOS, C.NOMBRE AS CURSO
//FROM INTERESADOS I
//INNER JOIN CURSOS C ON I.CODCURSO = C.CODCURSO
//GO

//SELECT * FROM V_INTERESADOS_CURSOS
#endregion
#region PROCEDURES
//CREATE PROCEDURE SP_INSERT_CLIENTEP
//(@NOMBRE NVARCHAR(50), @TLF NVARCHAR(10), @EMAIL NVARCHAR(20),
//@COMENTARIOS NVARCHAR(100), @CODCURSO NVARCHAR(5))
//AS
//    INSERT INTO INTERESADOS
//	VALUES ((SELECT MAX(IDINTERESADO) +1 FROM INTERESADOS),
//	@NOMBRE, @TLF, @EMAIL, @COMENTARIOS, @CODCURSO)
//GO
#endregion
namespace ProyectoERP.Repositories
{
    public class RepositoryERPSql : IRepo
    {
        private ErpContext context;
        public RepositoryERPSql(ErpContext context)
        {
            this.context = context;
        }
        public List<ClientePotencial> FindClientesP(string curso)
        {
            var consulta = from datos in this.context.ClientesPotenciales
                           where datos.Curso == curso
                           select datos;
            return consulta.ToList();
        }

        public ClientePotencial GetCliente(int idinteresado)
        {
            var consulta = from datos in this.context.ClientesPotenciales
                           where datos.IdInteresado==idinteresado
                           select datos;
            return consulta.FirstOrDefault();
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

        public async Task InsertClienteP(string nombrecliente, string tlf, string email, string? comentarios,string codcurso)
        {
            string sql = "SP_INSERT_CLIENTEP @ID, @NOMBRE, @TLF, @EMAIL, @COMENTARIOS, @CODCURSO";
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nombrecliente);
            SqlParameter pamtlf = new SqlParameter("@TLF", tlf);
            SqlParameter pamemail = new SqlParameter("@EMAIL", email);
            SqlParameter pamcomentarios = new SqlParameter("@COMENTARIOS", comentarios);
            SqlParameter pamcodcurso = new SqlParameter("@CODCURSO", codcurso);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamnombre, pamtlf, pamemail, pamcomentarios, pamcodcurso);
        }

        public async Task UpdateClienteP(int idinteresado, string nombrecliente, string tlf, string email, string comentarios)
        {
            throw new NotImplementedException();
        }

        Task IRepo.DeleteClienteP(int id)
        {
            throw new NotImplementedException();
        }
    }
}
