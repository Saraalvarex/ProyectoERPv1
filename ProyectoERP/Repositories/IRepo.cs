using ProyectoERP.Models;

namespace ProyectoERP.Repositories
{
    public interface IRepo
    {
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
        Task InsertClienteP(string nombrecliente, string tlf, string email, string? comentarios, string codcurso);
        Task UpdateClienteP(int idinteresado, string nombrecliente, string tlf, string email, string comentarios);
        Task <List<AlumnoPagos>> GetAlumnosPagos();
        Task<Usuario> ExisteUsuario(string nombreusuario, int idusuario);
        Task<List<AlumnoPagos>> FiltroNombreAlumnoAsync(string nombrealumno);
    }
}
