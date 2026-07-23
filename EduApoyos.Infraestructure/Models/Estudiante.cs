using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApoyos.Domain.Enums;
using EduApoyos.Domain.Entities;

namespace EduApoyos.Infraestructure.Models
{
    public class Estudiante
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public UsuarioEntity Usuario { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroDocumento { get; set; }

        [Required]
        public TipoDocumento TipoDocumento { get; set; }

        [Required]
        [StringLength(100)]
        public string ProgramaAcademico { get; set; }

        [Range(1, 12)]
        public int Semestre { get; set; }
    }
}
