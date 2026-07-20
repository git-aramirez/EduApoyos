using AutoMapper;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using EduApoyos.Domain.Entities;
using EduApoyos.Infraestructure.Models;
using EduApoyos.Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Infraestructure.Repositories
{
    public class UsuarioRepositorie : IUsuarioRepositorie
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioRepositorie(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsuarioEntity> AddAsync(UsuarioEntity usuarioEntity)
        {
            var model = _mapper.Map<Usuario>(usuarioEntity);
            await _context.Usuarios.AddAsync(model);
            await _context.SaveChangesAsync();
            var entity = _mapper.Map<UsuarioEntity>(model);

            return entity;
        }
    }
}
