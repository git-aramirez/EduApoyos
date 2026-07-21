using EduApoyos.Application.IRepositories;
using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Infraestructure.Repositories
{
    public class SolicitudApoyoRepository : ISolicitudApoyoRepository
    {


        public Task<SolicitudApoyoEntity> AddAsync(SolicitudApoyoEntity solicitudApoyoEntity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SolicitudApoyoEntity>> GetByEstadoAndTipoAsync(EstadoSolicitud estado, TipoApoyo tipoApoyo)
        {
            throw new NotImplementedException();
        }

        public Task<SolicitudApoyoEntity> GetByIdAsync(Guid solicitudId)
        {
            throw new NotImplementedException();
        }
    }
}
