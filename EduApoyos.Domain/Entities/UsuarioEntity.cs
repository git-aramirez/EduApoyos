using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Domain.Entities
{
    public class UsuarioEntity
    {
        public Guid Id { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public RolUsuario Rol { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
