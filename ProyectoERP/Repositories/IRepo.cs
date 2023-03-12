using ProyectoERP.Models;

namespace ProyectoERP.Repositories
{
    public interface IRepo
    {
        Usuario LoginUser(string username, string clave);
        List<ClientePotencial> GetClientesP();
        List<Curso> GetCursos();
        List<ClientePotencial> FindClientesP(string curso);
        ClientePotencial GetCliente(int id);
        Task InsertClienteP(string nombrecliente, string tlf, string email, string? comentarios, string codcurso);
        Task UpdateClienteP(int idinteresado, string nombrecliente, string tlf, string email, string comentarios);
    }
}
