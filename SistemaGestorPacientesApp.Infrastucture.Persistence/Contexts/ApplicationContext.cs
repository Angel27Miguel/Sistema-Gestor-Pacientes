using Microsoft.EntityFrameworkCore;
using SistemaGestorPacientesApp.Core.Domain.Common;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Cita> Citas { get; set; }
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<PruebaLaboratorio> PruebasLaboratorio { get; set; }
        public DbSet<ResultadoLaboratorio> ResultadosLaboratorio { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API

            #region "Tablas"

            modelBuilder.Entity<Cita>()
                .ToTable("Citas");

            modelBuilder.Entity<Consultorio>()
                .ToTable("Consultorios");

            modelBuilder.Entity<Medico>()
                .ToTable("Medicos");

            modelBuilder.Entity<Paciente>()
                .ToTable("Pacientes");

            modelBuilder.Entity<PruebaLaboratorio>()
                .ToTable("PruebasLaboratorio");

            modelBuilder.Entity<ResultadoLaboratorio>()
                .ToTable("ResultadosLaboratorio");

            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios");

            #endregion

            #region "Llaves primarias"

            modelBuilder.Entity<Cita>()
                .HasKey(cita => cita.Id);

            modelBuilder.Entity<Consultorio>()
                .HasKey(consultorio => consultorio.Id);

            modelBuilder.Entity<Medico>()
                .HasKey(medico => medico.Id);

            modelBuilder.Entity<Paciente>()
                .HasKey(paciente => paciente.Id);

            modelBuilder.Entity<PruebaLaboratorio>()
                .HasKey(prueba => prueba.Id);

            modelBuilder.Entity<ResultadoLaboratorio>()
                .HasKey(resultado => resultado.Id);

            modelBuilder.Entity<Usuario>()
                .HasKey(usuario => usuario.Id);

            #endregion

            #region "Relaciones"
            // Relacion entre Cita y Paciente
            modelBuilder.Entity<Cita>()
        .HasOne(c => c.Paciente)
        .WithMany(p => p.Citas) 
        .HasForeignKey(c => c.PacienteId)
        .OnDelete(DeleteBehavior.NoAction);

            // Relacion entre Cita y Medico
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Medico)
                .WithMany(m => m.Citas) 
                .HasForeignKey(c => c.MedicoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relacion entre Cita y Consultorio
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Consultorio)
                .WithMany()
                .HasForeignKey(c => c.ConsultorioId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Relacion entre Paciente y Consultorio
            modelBuilder.Entity<Paciente>()
                .HasOne(paciente => paciente.Consultorio)
                .WithMany(consultorio => consultorio.Pacientes)
                .HasForeignKey(paciente => paciente.ConsultorioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Medico y Consultorio
            modelBuilder.Entity<Medico>()
                .HasOne(medico => medico.Consultorio)
                .WithMany(consultorio => consultorio.Medicos)
                .HasForeignKey(medico => medico.ConsultorioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre ResultadoLaboratorio y Cita
            modelBuilder.Entity<ResultadoLaboratorio>()
                .HasOne(resultado => resultado.Cita)
                .WithMany()
                .HasForeignKey(resultado => resultado.CitaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacion entre ResultadoLaboratorio y Paciente 
            modelBuilder.Entity<ResultadoLaboratorio>()
                .HasOne(resultado => resultado.Paciente)
                .WithMany()
                .HasForeignKey(resultado => resultado.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacion entre ResultadoLaboratorio y  Prueba
            modelBuilder.Entity<ResultadoLaboratorio>()
                .HasOne(resultado => resultado.Prueba)
                .WithMany() 
                .HasForeignKey(resultado => resultado.PruebaLaboratorioId)
                .OnDelete(DeleteBehavior.Restrict);


            #endregion

            #region "Configuraciones de propiedades"

            // Cita
            modelBuilder.Entity<Cita>()
                .Property(cita => cita.Causa)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Cita>()
                .Property(cita => cita.FechaCita)
                .IsRequired();

            modelBuilder.Entity<Cita>()
                .Property(cita => cita.HoraCita)
                .IsRequired();

            // Paciente
            modelBuilder.Entity<Paciente>()
                .Property(paciente => paciente.Cedula)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Paciente>()
                .Property(paciente => paciente.Telefono)
                .HasMaxLength(15);

            modelBuilder.Entity<Paciente>()
                .Property(paciente => paciente.Direccion)
                .HasMaxLength(500);

            modelBuilder.Entity<Paciente>()
                .Property(paciente => paciente.Foto)
                .HasMaxLength(500);

            // Medico
            modelBuilder.Entity<Medico>()
                .Property(medico => medico.Cedula)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Medico>()
                .Property(medico => medico.Telefono)
                .HasMaxLength(15);

            modelBuilder.Entity<Medico>()
                .Property(medico => medico.Correo)
                .IsRequired()
                .HasMaxLength(100);

            // Consultorio
            modelBuilder.Entity<Consultorio>()
                .Property(consultorio => consultorio.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            // Usuario
            modelBuilder.Entity<Usuario>()
                .Property(usuario => usuario.NombreUsuario)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Usuario>()
                .Property(usuario => usuario.Correo)
                .IsRequired()
                .HasMaxLength(100);

            #endregion
        }
    }
}
