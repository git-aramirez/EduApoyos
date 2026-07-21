using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.DTOs.Requests
{
    public class EstudianteRequestDto
    {
        [Required(ErrorMessage = "El Id del usuario es obligatorio")]
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "El numero de documento es obligatorio")]
        [StringLength(20, ErrorMessage = "El número de documento no puede superar los 20 caracteres")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El número de documento solo puede contener dígitos")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public TipoDocumento TipoDocumento { get; set; }

        [Required(ErrorMessage = "El programa academico es obligatorio")]
        [StringLength(100, ErrorMessage = "El número de documento no puede superar los 100 caracteres")]
        public string ProgramaAcademico { get; set; } = string.Empty;

        [Range(1, 12, ErrorMessage = "El semestre debe estar entre 1 y 12")]
        public int Semestre { get; set; }
    }
}
