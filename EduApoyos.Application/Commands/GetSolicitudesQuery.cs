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
    public class GetSolicitudesQuery : IRequest<IEnumerable<SolicitudApoyoResponseDto>>
    {
        public EstadoSolicitud? Estado { get; set; }
        public TipoApoyo? TipoApoyo { get; set; }
    }
}
