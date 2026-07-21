using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.Mappings
{
    public class EstudianteAppProfile: Profile
    {
        public EstudianteAppProfile()
        {
            CreateMap<EstudianteRequestDto, EstudianteEntity>()
             .ConstructUsing(src => new EstudianteEntity(
                 src.UsuarioId,
                 src.NumeroDocumento.ToUpper(),
                 src.TipoDocumento,
                 src.ProgramaAcademico,
                 src.Semestre
             ));

            CreateMap<EstudianteEntity, EstudianteResponseDto>();
        }
    }
}
