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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository=usuarioRepository;
            _mapper=mapper;
        }

        public async Task<UsuarioResponseDto> AddAsync(UsuarioRequestDto usuarioRequestDto)
        {
            var usuarioEntity = _mapper.Map<UsuarioEntity>(usuarioRequestDto);

            var result = await _usuarioRepository.AddAsync(usuarioEntity);

            return _mapper.Map<UsuarioResponseDto>(result);
        }
    }
}
