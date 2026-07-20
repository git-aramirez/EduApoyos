using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApoyos.Domain.Enums;

namespace EduApoyos.Domain.Entities
{
    public class EstudianteEntity
    {
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }

        public UsuarioEntity Usuario { get; set; }

        public string NumeroDocumento { get; set; } = string.Empty;

        public TipoDocumento TipoDocumento { get; set; }

        public string ProgramaAcademico { get; set; } = string.Empty;

        public int Semestre { get; set; }
    }
}
