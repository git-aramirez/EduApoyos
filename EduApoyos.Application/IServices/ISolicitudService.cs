using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.IServices
{
    public interface ISolicitudService
    {
        Task<IEnumerable<SolicitudApoyoResponseDto>> GetByEstadoAndTipoAsync(EstadoSolicitud? estado, TipoApoyo? tipoApoyo);
        Task<SolicitudApoyoResponseDto> AddAsync(SolicitudApoyoRequestDto solicitudApoyoRequestDto);
        Task<SolicitudDetalleResponseDto> GetByIdAsync(Guid solicitudId);
        Task<SolicitudApoyoResponseDto> PatchEstadoAsync(Guid solicitudId, SolicitudCambioEstadoRequestDto solicitudCambioEstadoRequestDto);
    }
}
