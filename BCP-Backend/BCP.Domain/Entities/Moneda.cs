using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCP.Domain.Entities
{
    public class Moneda
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [Column(TypeName = "char(3)")]
        public string CodigoSunat { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Nombre { get; set; }

        public ICollection<TipoCambio> TipoCambios { get; set; }
    }
}