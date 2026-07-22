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
    public class HistorialEstadoRepository : IHistorialEstadoRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HistorialEstadoRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HistorialEstadoEntity> AddAsync(HistorialEstadoEntity historialEstadoEntity)
        {
            var model = _mapper.Map<HistorialEstado>(historialEstadoEntity);
            await _context.HistorialEstados.AddAsync(model);
            await _context.SaveChangesAsync();
            var entity = _mapper.Map<HistorialEstadoEntity>(model);

            return entity;
        }

        public async Task<IEnumerable<HistorialEstadoEntity>> GetBySolicitudIdAsync(Guid solicitudId)
        {
            var historial = await _context.HistorialEstados
                .Include(h => h.Usuario) 
                .Where(h => h.SolicitudId == solicitudId)
                .OrderByDescending(h => h.FechaCambio)
                .ToListAsync();

            return _mapper.Map<IEnumerable<HistorialEstadoEntity>>(historial);
        }
    }
}
