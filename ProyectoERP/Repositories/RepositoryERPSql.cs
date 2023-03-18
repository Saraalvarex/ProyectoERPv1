using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoERP.Data;
using ProyectoERP.Helpers;
using ProyectoERP.Models;

#region VISTAS y PROCEDURES
//CREATE VIEW V_INTERESADOS_CURSOS
//AS
//SELECT I.IDINTERESADO, I.NOMBRE, I.TLF, I.EMAIL, I.COMENTARIOS, C.NOMBRE AS CURSO
//FROM INTERESADOS I
//INNER JOIN CURSOS C ON I.CODCURSO = C.CODCURSO
//GO

//SELECT * FROM V_INTERESADOS_CURSOS

//CREATE PROCEDURE SP_INSERT_CLIENTEP
//(@NOMBRE NVARCHAR(50), @TLF NVARCHAR(10), @EMAIL NVARCHAR(50),
//@COMENTARIOS NVARCHAR(100), @CODCURSO NVARCHAR(5))
//AS
//    INSERT INTO INTERESADOS
//	VALUES ((SELECT MAX(IDINTERESADO) +1 FROM INTERESADOS),
//	@NOMBRE, @TLF, @EMAIL, @COMENTARIOS, @CODCURSO)
//GO

//PARA VISTA GRUPOS. PENDIENTE VER SI HAGO PAGINACION Y NO HAGO VISTA
//SELECT G.CODGRUPO, A.NOMBRE AS ALUMNO, C.NOMBRE AS CURSO,
//G.CODTURNO AS TURNO, G.DIAS, G.FECHAINICIO FROM GRUPOS G
//INNER JOIN ALUMNOS A ON G.IDALUMNO = A.IDALUMNO
//INNER JOIN CURSOS C ON C.CODCURSO = G.CODCURSO

//PARA VISTA ALUMNOS.
//SELECT AG.CODGRUPO, A.FOTO, A.NOMBRE, C.MATRICULA, AG.MONTOPAGADO AS PAGADO,
//SUM(C.PRECIO + C.MATRICULA) AS PENDIENTE
//FROM ALUMNOS_GRUPOS AG
//INNER JOIN ALUMNOS A ON AG.IDALUMNO = A.IDALUMNO
//INNER JOIN GRUPOS G ON G.CODGRUPO = AG.CODGRUPO
//INNER JOIN CURSOS C ON C.CODCURSO = G.CODCURSO
//WHERE AG.CODGRUPO='G002'
//GROUP BY AG.CODGRUPO, A.FOTO, A.NOMBRE, C.MATRICULA, AG.MONTOPAGADO;

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
        private int GetMaxIdUsuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(x => x.IdUsuario) + 1;
            }
        }

        public async Task RegisterUser(string nombreusuario, string email, string clave, string rol, string foto)
        {
                Usuario user = new Usuario();
                user.IdUsuario = this.GetMaxIdUsuario();
                user.NombreUsuario = nombreusuario;
                user.Email = email;
                user.Rol = rol;
                user.Foto = foto;
                //Cada usuario tendrá un salt diferente
                user.Salt = HelperAuth.GenerarSalt();
                //Clave sin cifrar
                user.Clave = clave;
                //Ciframos clave con su salt
                user.ClaveEncrip = HelperAuth.EncriptarClave(clave, user.Salt);
                this.context.Usuarios.Add(user);
                await this.context.SaveChangesAsync();
        }
        //SIN FOTO
        public async Task RegisterUser(string nombreusuario, string email, string clave, string rol)
        {
            Usuario user = new Usuario();
            user.IdUsuario = this.GetMaxIdUsuario();
            user.NombreUsuario = nombreusuario;
            user.Email = email;
            user.Rol = rol;
            user.Foto = null;
            //Cada usuario tendrá un salt diferente
            user.Salt = HelperAuth.GenerarSalt();
            //Clave sin cifrar
            user.Clave = clave;
            //Ciframos clave con su salt
            user.ClaveEncrip = HelperAuth.EncriptarClave(clave, user.Salt);
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }
        public Usuario LoginUser(string nombreusuario, string clave)
        {
            Usuario user = this.context.Usuarios.FirstOrDefault(x => x.NombreUsuario == nombreusuario);
            if (user == null)
            {
                return null;
            }
            else
            {
                //Recuperar password cifrado de la bbd
                byte[] claveuser = user.ClaveEncrip;
                //Debemos cifrar de nuevo el password de usuario
                //junto a su salt utilizando la misma tecnica
                string salt = user.Salt;
                byte[] temp = HelperAuth.EncriptarClave(clave, salt);
                bool respuesta = HelperAuth.CompararClaves(claveuser, temp);
                if (respuesta == true)
                {
                    //GUARDAR SESION AQUÍ
                    return user;
                }
                else
                {
                    return null;
                }
            }
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
        public async Task<List<Alumno>> GetAlumnos()
        {
            return await this.context.Alumnos.ToListAsync();
        }

        public List<Curso> GetCursos()
        {
            var cursos = from datos in this.context.Cursos
                         select datos;
            return cursos.ToList();
        }

        public async Task InsertClienteP(string nombrecliente, string tlf, string email, string? comentarios, string codcurso)
        {
            string sql = "SP_INSERT_CLIENTEP @NOMBRE, @TLF, @EMAIL, @COMENTARIOS, @CODCURSO";
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
    }
}
