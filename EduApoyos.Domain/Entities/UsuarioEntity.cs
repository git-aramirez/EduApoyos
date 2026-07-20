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

        public string NombreCompleto { get; set; }

        public string Email { get; set; } 

        public string PasswordHash { get; set; } 

        public RolUsuario Rol { get; set; }

        public DateTime FechaRegistro { get; set; } 

        public UsuarioEntity(string nombreCompleto, string email, string passwordHash, RolUsuario rol)
        {
            Id = Guid.NewGuid();
            NombreCompleto = nombreCompleto;
            Email = email;
            PasswordHash = passwordHash;
            Rol = rol;
            FechaRegistro = DateTime.UtcNow;
        }
    }
}
