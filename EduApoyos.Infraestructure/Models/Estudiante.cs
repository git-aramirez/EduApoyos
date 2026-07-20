using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApoyos.Domain.Enums;

namespace EduApoyos.Infraestructure.Models
{
    public class Estudiante
    {
        [Key]       
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario Usuario { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Required]
        public TipoDocumento TipoDocumento { get; set; }

        [Required]
        [StringLength(100)]
        public string ProgramaAcademico { get; set; } = string.Empty;

        [Range(1, 12, ErrorMessage = "El semestre debe estar entre 1 y 12")]
        public int Semestre { get; set; }
    }
}
