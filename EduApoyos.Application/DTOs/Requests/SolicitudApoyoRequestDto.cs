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
    public class SolicitudApoyoRequestDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EstudianteId { get; set; }

        [Required]
        public Guid AsesorId { get; set; }

        [Required]
        public TipoApoyo TipoApoyo { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser positivo")]
        public decimal MontoSolicitado { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public EstadoSolicitud Estado { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaSolicitud { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaActualizacion { get; set; }
    }
}
