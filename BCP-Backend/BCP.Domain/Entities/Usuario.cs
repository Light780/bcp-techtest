using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCP.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string NombreCompleto { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Correo { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(32)")]
        public string Password { get; set; }
        
    }
}