using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EduApoyos.Application.Mappings
{
    public class UsuarioAppProfile : Profile
    {
        public UsuarioAppProfile()
        {
            CreateMap<UsuarioRequestDto, UsuarioEntity>()
            .ConstructUsing(src => new UsuarioEntity(src.NombreCompleto, src.Email, src.UserName, src.Rol));

            CreateMap<UsuarioEntity, UsuarioResponseDto>();
        }
    }
}
