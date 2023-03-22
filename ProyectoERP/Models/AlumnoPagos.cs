﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoERP.Models
{
    [Table("V_ALUMNOS_PAGOS")]
    public class AlumnoPagos
    {
        [Key]
        [Column("CODGRUPO")]
        public string CodGrupo { get; set; }
        [Column("FOTO")]
        public string Foto { get; set; }
        [Column("NOMBRE")]
        public string NombreAlumno { get; set; }
        [Column("MATRICULA")]
        public decimal Matricula { get; set; }
        [Column("FINANCIACION")]
        public int Financiacion { get; set; }
        [Column("DURACION")]
        public int Duracion { get; set; }
        [Column("PAGADO")]
        public decimal Pagado { get; set; }
        [Column("PENDIENTE")]
        public decimal Pendiente { get; set; }
        
    }
}