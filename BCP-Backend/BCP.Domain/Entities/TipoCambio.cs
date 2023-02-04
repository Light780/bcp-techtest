using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCP.Domain.Entities
{
    public class TipoCambio
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(9,6)")]
        public decimal Compra { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(9,6)")]
        public decimal Venta { get; set; }
        
        [Required]
        [ForeignKey("Moneda")]
        public Guid IdMoneda { get; set; }
        public Moneda Moneda { get; set; }
        
    }
}