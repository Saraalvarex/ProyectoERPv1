using ProyectoERP.Models;

namespace ProyectoERP.Repositories
{
    public interface IRepo
    {
        Task<Usuario> ExisteUsuario(string nombreusuario, string clave);
        Task RegisterUser(string nombreusuario, string email, string clave, string rol, string foto);
        Task RegisterUser(string nombreusuario, string email, string clave, string rol);
        Usuario LoginUser(string nombreusuario, string clave);
        List<ClientePotencial> GetClientesP();
        List<Curso> GetCursos();
        List<ClientePotencial> FindClientesP(string curso);
        ClientePotencial GetCliente(int id);
        Task <List<Grupo>> GetGrupos();
        Task<List<Grupo>> FiltroGruposCurso(string curso);
        Task<Grupo> FiltroGruposCod(string codgrupo);
        Task<List<Grupo>> FiltroGruposFecha(DateTime fecha);
        Task InsertGrupo(string codcurso, string turno, string dias, string fechainicio);
        Task InsertClienteP(string nombrecliente, string tlf, string email, string? comentarios, string codcurso);
        Task UpdateClienteP(int idinteresado, string nombrecliente, string tlf, string email, string comentarios);
        Task<List<AlumnoPagos>> GetAlumnosPagos();
        Task<List<AlumnoPagos>> FiltroNombreAlumnoAsync(string nombrealumno);
        Task<List<AlumnoPagos>> FiltroAlumnosPagosFecha(DateTime fechainicio);
        Task<List<AlumnoPagos>> FiltroAlumnosPagosGrupoAsync(string codgrupo);
        Task<List<AlumnoPagos>> GetAlumnosGrupoAsync(string codgrupo);
        Task<Alumno> GetAlumno(int idalumno);
        Task<int> InsertFactAsync(int idalumno, string rutafact);
    }
}
