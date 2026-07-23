using EduApoyos.Domain.Entities;
using EduApoyos.Infraestructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Infraestructure.Persistence
{
    public class AppDbContext: IdentityDbContext<UsuarioEntity, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<HistorialEstado> HistorialEstados { get; set; }
        public DbSet<SolicitudApoyo> SolicitudesApoyo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<Estudiante>().ToTable("Estudiante");
            modelBuilder.Entity<HistorialEstado>().ToTable("HistorialEstado");
        }
    }
}
