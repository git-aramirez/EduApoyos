using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.DTOs.Responses
{
    public class SolicitudApoyoResponseDto
    {
        public Guid Id { get; set; }
        public EstudianteResponseDto Estudiante { get; set; }
        public UsuarioResponseDto Asesor { get; set; }
        public TipoApoyo TipoApoyo { get; set; }
        public decimal MontoSolicitado { get; set; }
        public string Descripcion { get; set; }
        public EstadoSolicitud Estado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public IEnumerable<HistorialEstadoResponseDto> historialEstados { get; set; }
    }
}
