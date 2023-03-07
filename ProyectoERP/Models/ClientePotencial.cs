using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoERP.Models
{
    [Table("V_INTERESADOS_CURSO")]
    public class ClientePotencial
    {
        [Key]
        [Column("IDINTERESADO")]
        public int IdInteresado { get; set;}
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("TLF")]
        public int Tlf { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("COMENTARIOS")]
        public string Coments { get; set; }
        [Column("CURSO")]
        public string Curso { get; set; }
    }
}
