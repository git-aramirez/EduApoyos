using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.Commands
{
    public class GetSolicitudesQueryHandler
    : IRequestHandler<GetSolicitudesQuery, IEnumerable<SolicitudApoyoResponseDto>>
    {
        private readonly ISolicitudApoyoRepository _solicitudRepo;

        public GetSolicitudesQueryHandler(ISolicitudApoyoRepository solicitudRepo)
        {
            _solicitudRepo = solicitudRepo;
        }

        public async Task<IEnumerable<SolicitudApoyoResponseDto>> Handle(GetSolicitudesQuery request, CancellationToken cancellationToken)
        {
            // Obtener todas las solicitudes
            var solicitudes = await _solicitudRepo.GetByEstadoAndTipoAsync();

            // Aplicar filtros si existen
            if (request.Estado.HasValue)
                solicitudes = solicitudes.Where(s => s.Estado == request.Estado.Value);

            if (request.TipoApoyo.HasValue)
                solicitudes = solicitudes.Where(s => s.TipoApoyo == request.TipoApoyo.Value);

            // Mapear a DTO
            return solicitudes.Select(s => new SolicitudApoyoResponseDto
            {
                Id = s.Id,
                Estado = s.Estado,
                TipoApoyo = s.TipoApoyo,
                MontoSolicitado = s.MontoSolicitado,
                FechaSolicitud = s.FechaSolicitud
            });
        }
    }
}
