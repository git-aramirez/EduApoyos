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
    public class EstudianteInfraProfile: Profile
    {
        public EstudianteInfraProfile()
        {
            CreateMap<EstudianteEntity, Estudiante>();
            CreateMap<Estudiante, EstudianteEntity>();
        }
    }
}
