using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using EduApoyos.Application.IServices;
using EduApoyos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudienteRepository _estudienteRepository;
        private readonly IMapper _mapper;

        public EstudianteService(IEstudienteRepository estudienteRepository, IMapper mapper)
        {
            _estudienteRepository=estudienteRepository;
            _mapper=mapper;
        }

        public async Task<EstudianteResponseDto> AddAsync(EstudianteRequestDto estudianteRequestDto)
        {
            var estudianteEntity = _mapper.Map<EstudianteEntity>(estudianteRequestDto);
            var response = await _estudienteRepository.AddAsync(estudianteEntity);
            var estudianteResponseDto = _mapper.Map<EstudianteResponseDto>(response);

            return estudianteResponseDto;
        }

        public async Task<IEnumerable<EstudianteResponseDto>> GetAllAsync()
        {
            var estudiantes = await _estudienteRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<EstudianteResponseDto>>(estudiantes);
        }

        public async Task<IEnumerable<SolicitudApoyoResponseDto>> GetSolicitudesByEstudianteIdAsync(Guid estudianteId)
        {
            var solicitudes = await _estudienteRepository.GetSolicitudesByEstudianteIdAsync(estudianteId);

            return _mapper.Map<IEnumerable<SolicitudApoyoResponseDto>>(solicitudes);
        }
    }
}
