using EduApoyos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Application.DTOs.Responses
{
    public class EstudianteResponseDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string NumeroDocumento { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string ProgramaAcademico { get; set; }
        public int Semestre { get; set; }
    }
}
