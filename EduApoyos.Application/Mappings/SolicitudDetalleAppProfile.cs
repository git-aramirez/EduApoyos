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
    public class SolicitudDetalleAppProfile: Profile
    {
        public SolicitudDetalleAppProfile()
        {
            CreateMap<SolicitudApoyoEntity, SolicitudApoyoResponseDto>();
        }
    }
}
