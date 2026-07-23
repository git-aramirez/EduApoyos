using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using EduApoyos.Application.IServices;
using EduApoyos.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEstudienteRepository _estudienteRepository;
        private readonly IMapper _mapper;

        public EstudianteService(IEstudienteRepository estudienteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _estudienteRepository=estudienteRepository;
            _mapper=mapper;
            _httpContextAccessor=httpContextAccessor;
        }

        public async Task<EstudianteResponseDto> AddAsync(EstudianteRequestDto estudianteRequestDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var estudianteEntity = _mapper.Map<EstudianteEntity>(estudianteRequestDto);
            estudianteEntity.UsuarioId = Guid.Parse(userId);
            var response = await _estudienteRepository.AddAsync(estudianteEntity);
            var estudianteResponseDto = _mapper.Map<EstudianteResponseDto>(response);

            return estudianteResponseDto;
        }

        public async Task<IEnumerable<EstudianteResponseDto>> GetAllAsync()
        {
            var estudiantes = await _estudienteRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<EstudianteResponseDto>>(estudiantes);
        }

        public async Task<IEnumerable<SolicitudApoyoResponseDto>> GetSolicitudesByEstudianteIdAsync(Guid id)
        {
            var estudiante = await _estudienteRepository.GetByIdAsync(id);
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) 
                return new List<SolicitudApoyoResponseDto>();

            if (estudiante == null || estudiante.UsuarioId != Guid.Parse(userId))
            {
                throw new UnauthorizedAccessException("No puedes acceder a estas solicitudes");
            }
            var solicitudes = await _estudienteRepository.GetSolicitudesByEstudianteIdAsync(id);

            return _mapper.Map<IEnumerable<SolicitudApoyoResponseDto>>(solicitudes);
        }
    }
}
