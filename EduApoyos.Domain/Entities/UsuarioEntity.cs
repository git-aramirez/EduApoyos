using EduApoyos.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Domain.Entities
{
    public class UsuarioEntity: IdentityUser<Guid>
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public UsuarioEntity(string nombreCompleto, string email, string userName, RolUsuario rol)
        {
            Id = Guid.NewGuid();
            NombreCompleto = nombreCompleto;
            Email = email;
            Rol = rol;
            FechaRegistro = DateTime.UtcNow;
            UserName = userName;
        }
    }
}
