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
    class SolicitudApoyoAppProfile: Profile
    {
        public SolicitudApoyoAppProfile()
        {
            CreateMap<SolicitudApoyoRequestDto, SolicitudApoyoEntity>()
                .ConstructUsing(src => new SolicitudApoyoEntity(
                    src.EstudianteId,
                    src.AsesorId,
                    src.TipoApoyo,
                    src.MontoSolicitado,
                    src.Descripcion
                ));

            CreateMap<SolicitudApoyoEntity, SolicitudApoyoResponseDto>();
        }
    }
}
