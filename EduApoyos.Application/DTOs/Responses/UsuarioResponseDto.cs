using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.DTOs.Responses
{
    public class UsuarioResponseDto
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public RolUsuario Rol { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
