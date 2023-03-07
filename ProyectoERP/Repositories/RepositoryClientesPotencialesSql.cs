using ProyectoERP.Data;
using ProyectoERP.Models;

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
