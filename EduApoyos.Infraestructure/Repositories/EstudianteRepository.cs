using AutoMapper;
using EduApoyos.Application.IRepositories;
using EduApoyos.Domain.Entities;
using EduApoyos.Infraestructure.Models;
using EduApoyos.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Infraestructure.Repositories
{
    public class EstudianteRepository : IEstudienteRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EstudianteRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EstudianteEntity> AddAsync(EstudianteEntity estudianteEntity)
        {
            var model = _mapper.Map<Estudiante>(estudianteEntity);
            await _context.Estudiantes.AddAsync(model);
            await _context.SaveChangesAsync();
            var entity = _mapper.Map<EstudianteEntity>(model);

            return entity;
        }

        public async Task<IEnumerable<EstudianteEntity>> GetAllAsync()
        {
            IEnumerable<Estudiante> estudientes = await _context.Estudiantes.ToListAsync();

            return _mapper.Map<IEnumerable<EstudianteEntity>>(estudientes);
        }

        public async Task<IEnumerable<SolicitudApoyoEntity>> GetSolicitudesByEstudianteIdAsync(Guid estudianteId)
        {
            IEnumerable<SolicitudApoyo> solicitudes = await _context.SolicitudesApoyo
                                                        .Where(s => s.EstudianteId == estudianteId)
                                                        .ToListAsync();

            return _mapper.Map<IEnumerable<SolicitudApoyoEntity>>(solicitudes);

        }
    }
}
