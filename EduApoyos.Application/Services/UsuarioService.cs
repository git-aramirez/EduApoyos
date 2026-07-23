using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using EduApoyos.Application.IServices;
using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace EduApoyos.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IConfiguration config)
        {
            _usuarioRepository=usuarioRepository;
            _mapper=mapper;
            _config=config;
        }

        public async Task<UsuarioResponseDto> AddAsync(UsuarioRequestDto usuarioRequestDto)
        {
            var usuarioEntity = _mapper.Map<UsuarioEntity>(usuarioRequestDto);

            var result = await _usuarioRepository.AddAsync(usuarioEntity, usuarioRequestDto.Password);

            return _mapper.Map<UsuarioResponseDto>(result);
        }

        public async Task<(string?, string?)> LoginAsync(LoginDto loginDto)
        {
            var user = await _usuarioRepository.GetByUserNameAsync(loginDto.Username);

            if (user != null && await _usuarioRepository.CheckPasswordAsync(user, loginDto.Password))
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("NombreCompleto", user.NombreCompleto),
                new Claim(ClaimTypes.Role, user.Rol.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);

                return (new JwtSecurityTokenHandler().WriteToken(token), user.Rol.ToString()) ;
            }

            return (null, null);
        }
    }
}
