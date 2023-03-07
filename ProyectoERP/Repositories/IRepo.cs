using ProyectoERP.Models;

namespace ProyectoERP.Repositories
{
    public interface IRepo
    {
        List<ClientePotencial> GetClientesP();
        List<ClientePotencial> FindClientesP(string curso);
        ClientePotencial GetCliente(int id);
        void InsertClienteP(int id, string nombre, string tlf, string email, string comentarios);
        void UpdateClienteP(int id, string nombre, string tlf, string email, string comentarios);
        void DeleteClienteP(int id);
    }
}
