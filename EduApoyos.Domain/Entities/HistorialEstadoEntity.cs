using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Domain.Entities
{
    public class HistorialEstadoEntity
    {
        public Guid Id { get; set; }

        public Guid SolicitudId { get; set; }

        public SolicitudApoyoEntity Solicitud { get; set; }

        public Guid UsuarioId { get; set; }

        public UsuarioEntity Usuario { get; set; }

        public EstadoSolicitud EstadoAnterior { get; set; }

        public EstadoSolicitud EstadoNuevo { get; set; }

        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        public string Observacion { get; set; } = string.Empty;
    }
}
