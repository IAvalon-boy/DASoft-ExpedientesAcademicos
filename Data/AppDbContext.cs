using Microsoft.EntityFrameworkCore;
using SistemaExpedientesAcademicos.Models;

namespace SistemaExpedientesAcademicos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Materias> Materias { get; set; }
        public DbSet<Expedientes> Expedientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Alumno>().ToTable("Alumnos");
            modelBuilder.Entity<Materias>().ToTable("Materias");
            modelBuilder.Entity<Expedientes>().ToTable("Expedientes");

            
            modelBuilder.Entity<Expedientes>()
                .Property(e => e.NotaFinal)
                .HasPrecision(3, 1);

            
            modelBuilder.Entity<Expedientes>()
                .HasOne(e => e.Alumno)
                .WithMany(a => a.Expedientes)
                .HasForeignKey(e => e.AlumnoId);

            modelBuilder.Entity<Expedientes>()
                .HasOne(e => e.Materia)
                .WithMany(m => m.Expedientes)
                .HasForeignKey(e => e.MateriaId);
        }
    }
}