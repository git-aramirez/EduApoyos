using AutoMapper;
using EduApoyos.Application.IRepositories;
using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
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
    public class SolicitudApoyoRepository : ISolicitudApoyoRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SolicitudApoyoRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SolicitudApoyoEntity> AddAsync(SolicitudApoyoEntity solicitudApoyoEntity)
        {
            var model = _mapper.Map<SolicitudApoyo>(solicitudApoyoEntity);
            await _context.SolicitudesApoyo.AddAsync(model);
            await _context.SaveChangesAsync();
            var entity = _mapper.Map<SolicitudApoyoEntity>(model);

            return entity;
        }

        public async Task<IEnumerable<SolicitudApoyoEntity>> GetByEstadoAndTipoAsync(EstadoSolicitud? estado, TipoApoyo? tipoApoyo)
        {
            IQueryable<SolicitudApoyo> query = _context.SolicitudesApoyo
                                                       .Include(s => s.Estudiante)
                                                       .Include(s => s.Asesor);
            if (estado.HasValue)
                query = query.Where(s => s.Estado == estado.Value);

            if (tipoApoyo.HasValue)
                query = query.Where(s => s.TipoApoyo == tipoApoyo.Value);

            var solicitudes = await query.ToListAsync();

            return _mapper.Map<IEnumerable<SolicitudApoyoEntity>>(solicitudes);
        }

        public async Task<SolicitudApoyoEntity> GetByIdAsync(Guid solicitudId)
        {
            var solicitud = await _context.SolicitudesApoyo
                .Include(s => s.Estudiante)
                .Include(s => s.Asesor)
                .FirstOrDefaultAsync(s => s.Id == solicitudId);

            return _mapper.Map<SolicitudApoyoEntity>(solicitud);
        }

        public async Task<SolicitudApoyoEntity> PatchEstadoAsync(SolicitudApoyoEntity solicitudApoyoEntity, EstadoSolicitud estadoSolicitud)
        {
            var solicitud = _mapper.Map<SolicitudApoyo>(solicitudApoyoEntity);
            _context.SolicitudesApoyo.Attach(solicitud);
            _context.Entry(solicitud).Property(s => s.Estado).CurrentValue = estadoSolicitud;
            _context.Entry(solicitud).Property(s => s.FechaActualizacion).CurrentValue = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            solicitudApoyoEntity.Estado = estadoSolicitud;
            solicitudApoyoEntity.FechaActualizacion = solicitud.FechaActualizacion;

            return solicitudApoyoEntity;
        }
    }
}
