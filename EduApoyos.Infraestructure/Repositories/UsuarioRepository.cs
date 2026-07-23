using AutoMapper;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using EduApoyos.Domain.Entities;
using EduApoyos.Infraestructure.Models;
using EduApoyos.Infraestructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Infraestructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<UsuarioEntity> _userManager;

        public UsuarioRepository(UserManager<UsuarioEntity> userManager)
        {
            _userManager=userManager;
        }

        public async Task<UsuarioEntity> AddAsync(UsuarioEntity usuarioEntity, string password)
        {
            var result = await _userManager.CreateAsync(usuarioEntity, password);

            if (!result.Succeeded)
            {
                var errores = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"No se pudo crear el usuario: {errores}");
            }

            return usuarioEntity;
        }

        public async Task<UsuarioEntity> GetByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> CheckPasswordAsync(UsuarioEntity user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
