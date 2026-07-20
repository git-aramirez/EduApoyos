using EduApoyos.Infraestructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Infraestructure.Persistence.Configuration
{
    public class SolicitudApoyoConfiguration : IEntityTypeConfiguration<SolicitudApoyo>
    {
        public void Configure(EntityTypeBuilder<SolicitudApoyo> builder)
        {
            builder.ToTable("SolicitudApoyo");

            builder.HasOne(s => s.Estudiante)
                   .WithMany()
                   .HasForeignKey(s => s.EstudianteId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Asesor)
                   .WithMany()
                   .HasForeignKey(s => s.AsesorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
