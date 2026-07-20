using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Infraestructure.Models
{
    public class HistorialEstado
    {
        [Key]        
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid SolicitudId { get; set; }

        [ForeignKey(nameof(SolicitudId))]
        public SolicitudApoyo Solicitud { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario Usuario { get; set; }

        [Required]
        public EstadoSolicitud EstadoAnterior { get; set; }

        [Required]
        public EstadoSolicitud EstadoNuevo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string Observacion { get; set; } = string.Empty;
    }
}
