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
    public class SolicitudApoyo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid EstudianteId { get; set; }

        [ForeignKey(nameof(EstudianteId))]
        public Estudiante Estudiante { get; set; }

        [Required]
        public Guid AsesorId { get; set; }

        [ForeignKey(nameof(AsesorId))]
        public Usuario Asesor { get; set; }

        [Required]
        public TipoApoyo TipoApoyo { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser positivo")]
        public decimal MontoSolicitado { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Pendiente;

        [DataType(DataType.DateTime)]
        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    }
}
