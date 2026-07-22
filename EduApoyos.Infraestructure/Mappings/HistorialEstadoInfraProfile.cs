using AutoMapper;
using EduApoyos.Domain.Entities;
using EduApoyos.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Infraestructure.Mappings
{
    public class HistorialEstadoInfraProfile: Profile
    {
        public HistorialEstadoInfraProfile()
        {
            CreateMap<HistorialEstadoEntity, HistorialEstado>();
            CreateMap<HistorialEstado, HistorialEstadoEntity>();
        }
    }
}
