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
    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudApoyoRepository _solicitudApoyoRepository;
        private readonly IMapper _mapper;

        public SolicitudService(ISolicitudApoyoRepository solicitudApoyoRepository, IMapper mapper)
        {
            _solicitudApoyoRepository=solicitudApoyoRepository;
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

        public async Task<SolicitudApoyoResponseDto> GetByIdAsync(Guid solicitudId)
        {
            var solicitud = await _solicitudApoyoRepository.GetByIdAsync(solicitudId);

            var solicitudApoyoResponse = _mapper.Map<SolicitudApoyoResponseDto>(solicitud); 

            if (solicitud!=null)
            {
            }
        }
    }
}
