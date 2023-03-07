using Microsoft.EntityFrameworkCore;
using ProyectoERP.Models;
//using System.Security.Cryptography.X509Certificates;

namespace ProyectoERP.Data
{
    public class ErpContext: DbContext
    {
        public ErpContext(DbContextOptions<ErpContext> options) : base(options) { }

        public DbSet<ClientePotencial> ClientesPotenciales { get; set; }
    }
    
}
