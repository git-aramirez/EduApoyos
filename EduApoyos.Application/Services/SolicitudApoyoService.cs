using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using EduApoyos.Application.IServices;
using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.Services
{
    public class SolicitudApoyoService : ISolicitudApoyoService
    {
        private readonly ISolicitudApoyoRepository _solicitudApoyoRepository;
        private readonly IHistorialEstadoRepository _historialEstadoRepository;
        private readonly IMapper _mapper;

        public SolicitudApoyoService(ISolicitudApoyoRepository solicitudApoyoRepository, IHistorialEstadoRepository historialEstadoRepository, IMapper mapper)
        {
            _solicitudApoyoRepository=solicitudApoyoRepository;
            _historialEstadoRepository=historialEstadoRepository;
            _mapper=mapper;
        }

        public async Task<SolicitudApoyoResponseDto> AddAsync(SolicitudApoyoRequestDto solicitudApoyoRequestDto)
        {
            var solicituApoyoEntity = _mapper.Map<SolicitudApoyoEntity>(solicitudApoyoRequestDto);

            var response = await _solicitudApoyoRepository.AddAsync(solicituApoyoEntity);

            return _mapper.Map<SolicitudApoyoResponseDto>(solicituApoyoEntity);
        }

        public async Task<IEnumerable<SolicitudApoyoResponseDto>> GetByEstadoAndTipoAsync(EstadoSolicitud? estado, TipoApoyo? tipoApoyo)
        {
            var solicitudes = await _solicitudApoyoRepository.GetByEstadoAndTipoAsync(estado, tipoApoyo);

            return _mapper.Map<IEnumerable<SolicitudApoyoResponseDto>>(solicitudes);
        }

        public async Task<SolicitudDetalleResponseDto> GetByIdAsync(Guid solicitudId)
        {
            var solicitud = await _solicitudApoyoRepository.GetByIdAsync(solicitudId);

            var solicitudDetalleResponse = _mapper.Map<SolicitudDetalleResponseDto>(solicitud); 

            if (solicitud!=null)
            {
                 var response = await _historialEstadoRepository.GetBySolicitudIdAsync(solicitudId);
                 var historialEstadosResponseDto = _mapper.Map<IEnumerable<HistorialEstadoResponseDto>>(response);

                solicitudDetalleResponse.historialEstados = historialEstadosResponseDto;
            }

            return solicitudDetalleResponse;
        }

        public async Task<SolicitudApoyoResponseDto> PatchEstadoAsync(Guid solicitudId, SolicitudCambioEstadoRequestDto solicitudCambioEstadoRequestDto)
        {
            var solicitud = await _solicitudApoyoRepository.GetByIdAsync(solicitudId);
            if (solicitud == null) throw new KeyNotFoundException();

            var response = await _solicitudApoyoRepository.PatchEstadoAsync(solicitud, solicitudCambioEstadoRequestDto.Estado);

            if (response!=null)
            {
                var historialEstado = new HistorialEstadoEntity(response.Id, response.AsesorId, solicitud.Estado, response.Estado, solicitudCambioEstadoRequestDto.Observacion);
                await _historialEstadoRepository.AddAsync(historialEstado);
            }

            return _mapper.Map<SolicitudApoyoResponseDto>(response);
        }
    }
}
