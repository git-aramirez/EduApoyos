using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.IRepositories
{
    public interface ISolicitudApoyoRepository
    {
        Task<SolicitudApoyoEntity> AddAsync(SolicitudApoyoEntity solicitudApoyoEntity);
        Task<IEnumerable<SolicitudApoyoEntity>> GetByEstadoAndTipoAsync(EstadoSolicitud? estado, TipoApoyo? tipoApoyo);
        Task<SolicitudApoyoEntity> GetByIdAsync(Guid solicitudId);
        Task<SolicitudApoyoEntity> PatchEstado(SolicitudApoyoEntity solicitudApoyoEntity, EstadoSolicitud estadoSolicitud);
    }
}
