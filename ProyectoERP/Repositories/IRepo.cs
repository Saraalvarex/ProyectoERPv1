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
        Task InsertClienteP(string nombrecliente, string tlf, string email, string? comentarios, string codcurso);
        Task UpdateClienteP(int idinteresado, string nombrecliente, string tlf, string email, string comentarios);
    }
}
