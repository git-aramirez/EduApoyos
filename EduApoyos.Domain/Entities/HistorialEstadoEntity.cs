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
        public Guid Id { get; private set; }
        public Guid SolicitudId { get; set; }
        public SolicitudApoyoEntity Solicitud { get; set; }
        public Guid UsuarioId { get; set; }
        public UsuarioEntity Usuario { get; set; }
        public EstadoSolicitud EstadoAnterior { get; set; }
        public EstadoSolicitud EstadoNuevo { get; set; }
        public DateTime FechaCambio { get; private set; }
        public string Observacion { get; set; } = string.Empty;

        public HistorialEstadoEntity(Guid solicitudId, Guid usuarioId,
                                     EstadoSolicitud estadoAnterior,
                                     EstadoSolicitud estadoNuevo,
                                     string observacion)
        {
            Id = Guid.NewGuid();
            SolicitudId = solicitudId;
            UsuarioId = usuarioId;
            EstadoAnterior = estadoAnterior;
            EstadoNuevo = estadoNuevo;
            Observacion = observacion;
            FechaCambio = DateTime.UtcNow;
        }
    }
}
