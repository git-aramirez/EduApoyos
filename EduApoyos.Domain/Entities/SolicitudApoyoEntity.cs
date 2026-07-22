using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApoyos.Domain.Enums;

namespace EduApoyos.Domain.Entities
{
    public class SolicitudApoyoEntity
    {
        public Guid Id { get; set; }
        public Guid EstudianteId { get; set; }
        public EstudianteEntity Estudiante { get; set; }
        public Guid AsesorId { get; set; }
        public UsuarioEntity Asesor { get; set; }
        public TipoApoyo TipoApoyo { get; set; }
        public decimal MontoSolicitado { get; set; }
        public string Descripcion { get; set; }
        public EstadoSolicitud Estado { get; set; }
        public DateTime FechaSolicitud { get; private set; }
        public DateTime FechaActualizacion { get;  set; }

        // Constructor parametrizado
        public SolicitudApoyoEntity(Guid estudianteId, Guid asesorId, TipoApoyo tipoApoyo,
                                    decimal montoSolicitado, string descripcion)
        {
            Id = Guid.NewGuid();
            EstudianteId = estudianteId;
            AsesorId = asesorId;
            TipoApoyo = tipoApoyo;
            MontoSolicitado = montoSolicitado;
            Descripcion = descripcion;
            Estado = EstadoSolicitud.Pendiente;
            FechaSolicitud = DateTime.UtcNow;
            FechaActualizacion = DateTime.UtcNow;
        }
    }
}
