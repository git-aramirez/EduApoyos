using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.DTOs.Responses
{
    public class HistorialEstadoResponseDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public UsuarioResponseDto Usuario { get; set; }

        public EstadoSolicitud EstadoAnterior { get; set; }

        public EstadoSolicitud EstadoNuevo { get; set; }

        public DateTime FechaCambio { get; set; }

        public string Observacion { get; set; }
    }
}
