using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.IServices
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto> AddAsync(UsuarioRequestDto usuarioRequestDto);

        Task<string?> LoginAsync(LoginDto loginDto);
    }
}
