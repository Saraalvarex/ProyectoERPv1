using ProyectoERP.Models;

namespace ProyectoERP.Repositories
{
    public interface IRepo
    {
        List<ClientePotencial> GetClientesP();
        List<string> GetCursos();
        List<ClientePotencial> FindClientesP(string curso);
        ClientePotencial GetCliente(int id);
        Task InsertClienteP(string nombrecliente, string tlf, string email, string? comentarios, string codcurso);
        Task UpdateClienteP(int idinteresado, string nombrecliente, string tlf, string email, string comentarios);
        Task DeleteClienteP(int id);
    }
}
