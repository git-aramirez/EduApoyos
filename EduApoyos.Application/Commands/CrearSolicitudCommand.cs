using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.Commands
{
    public class CrearSolicitudCommand : IRequest<SolicitudApoyoResponseDto>
    {
        public Guid EstudianteId { get; set; }
        public TipoApoyo TipoApoyo { get; set; }
        public decimal MontoSolicitado { get; set; }
    }
}
