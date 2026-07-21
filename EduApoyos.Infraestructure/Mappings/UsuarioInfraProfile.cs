using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Domain.Entities;
using EduApoyos.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EduApoyos.Application.Mappings
{
    public class UsuarioInfraProfile : Profile
    {
        public UsuarioInfraProfile()
        {
            CreateMap<UsuarioEntity, Usuario>();
            CreateMap<Usuario, UsuarioEntity>();
        }
    }
}
