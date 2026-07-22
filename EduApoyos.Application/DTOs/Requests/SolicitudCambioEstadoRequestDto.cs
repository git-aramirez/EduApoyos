using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.DTOs.Requests
{
    public class SolicitudCambioEstadoRequestDto
    {
        [Required]
        public EstadoSolicitud Estado { get; set; }

        [Required]
        public string Observacion { get; set; }
    }
}
