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

        public string NumeroDocumento { get; set; }

        public TipoDocumento TipoDocumento { get; set; }

        public string ProgramaAcademico { get; set; }

        public int Semestre { get; set; }

        public EstudianteEntity(Guid usuarioId, string numeroDocumento, TipoDocumento tipoDocumento,
                            string programaAcademico, int semestre)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            NumeroDocumento = numeroDocumento;
            TipoDocumento = tipoDocumento;
            ProgramaAcademico = programaAcademico;
            Semestre = semestre;
        }

        public EstudianteEntity()
        {
            
        }
    }
}
