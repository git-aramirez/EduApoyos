using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.Commands
{
    public class CrearSolicitudCommandHandler: IRequestHandler<CrearSolicitudCommand, SolicitudApoyoResponseDto>
    {
        private readonly ISolicitudApoyoRepository _solicitudRepo;
        private readonly IEstudianteRepository _estudianteRepo;

        public CrearSolicitudCommandHandler( ISolicitudApoyoRepository solicitudRepo, IEstudianteRepository estudianteRepo)
        {
            _solicitudRepo = solicitudRepo;
            _estudianteRepo = estudianteRepo;
        }

        public async Task<SolicitudApoyoResponseDto> Handle(CrearSolicitudCommand request, CancellationToken cancellationToken)
        {
            var estudiante = await _estudianteRepo.GetByIdAsync(request.EstudianteId);
            if (estudiante == null)
                throw new ArgumentException("Estudiante no existe");

            if (request.MontoSolicitado <= 0)
                throw new ArgumentException("Monto inválido");

            var solicitud = new SolicitudApoyoEntity
            {
                Id = Guid.NewGuid(),
                EstudianteId = request.EstudianteId,
                TipoApoyo = request.TipoApoyo,
                MontoSolicitado = request.MontoSolicitado,
                Estado = EstadoSolicitud.Pendiente,
                FechaSolicitud = DateTime.UtcNow
            };

            await _solicitudRepo.AddAsync(solicitud);

            return new SolicitudApoyoResponseDto
            {
                Id = solicitud.Id,
                Estado = solicitud.Estado,
                FechaSolicitud = solicitud.FechaSolicitud
            };
        }
    }
}
