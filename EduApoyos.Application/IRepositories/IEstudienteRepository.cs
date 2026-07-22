using EduApoyos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.IRepositories
{
    public interface IEstudienteRepository
    {
        Task<IEnumerable<EstudianteEntity>> GetAllAsync();
        Task<IEnumerable<SolicitudApoyoEntity>> GetSolicitudesByEstudianteIdAsync(Guid id);
        Task<EstudianteEntity> AddAsync(EstudianteEntity estudianteEntity);
    }
}
