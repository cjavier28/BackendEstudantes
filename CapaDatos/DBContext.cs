using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ServicioGestionEstudiantes.Entidades;

namespace CapaDatos
{

    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Estudiante> Estudiantes { get; set; }
        public virtual DbSet<Materia> Materia { get; set; }
        public virtual DbSet<Profesor> Profesors { get; set; }

        public virtual DbSet<Programa> Programas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__B5007C2463A89C62");

                entity.ToTable("Estudiante");

                entity.Property(e => e.IdEstudiante)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.ApellidosEstudiante).HasMaxLength(100);
                entity.Property(e => e.Contrasena).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.NombresEstudiante).HasMaxLength(100);
                entity.Property(e => e.Salt).HasMaxLength(100);

                entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Estudiantes)
                    .HasForeignKey(d => d.IdPrograma)
                    .HasConstraintName("FK__Estudiant__IdPro__4BAC3F29");

                entity.HasMany(d => d.IdMateria).WithMany(p => p.IdEstudiantes)
                    .UsingEntity<Dictionary<string, object>>(
                        "EstudianteMaterium",
                        r => r.HasOne<Materia>().WithMany()
                            .HasForeignKey("IdMateria")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Estudiant__IdMat__70DDC3D8"),
                        l => l.HasOne<Estudiante>().WithMany()
                            .HasForeignKey("IdEstudiante")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Estudiant__IdEst__6FE99F9F"),
                        j =>
                        {
                            j.HasKey("IdEstudiante", "IdMateria").HasName("PK__Estudian__ABC10843F1DF0773");
                            j.ToTable("EstudianteMateria");
                            j.IndexerProperty<string>("IdEstudiante")
                                .HasMaxLength(20)
                                .IsUnicode(false);
                        });
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(e => e.IdMateria).HasName("PK__Materia__EC1746708A7CE0DC");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(e => e.IdProfesor).HasName("PK__Profesor__C377C3A17EAD775A");

                entity.ToTable("Profesor");

                entity.Property(e => e.IdProfesor)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.ApellidosProfesor).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.NombresProfesor).HasMaxLength(100);

                entity.HasMany(d => d.IdMateria).WithMany(p => p.IdProfesors)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProfesorMaterium",
                        r => r.HasOne<Materia>().WithMany()
                            .HasForeignKey("IdMateria")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__ProfesorM__IdMat__571DF1D5"),
                        l => l.HasOne<Profesor>().WithMany()
                            .HasForeignKey("IdProfesor")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__ProfesorM__IdPro__5629CD9C"),
                        j =>
                        {
                            j.HasKey("IdProfesor", "IdMateria").HasName("PK__Profesor__DDB6B7C60CD6A5BE");
                            j.ToTable("ProfesorMateria");
                            j.IndexerProperty<string>("IdProfesor")
                                .HasMaxLength(20)
                                .IsUnicode(false);
                        });
            });

            modelBuilder.Entity<Programa>(entity =>
            {
                entity.HasKey(e => e.IdPrograma).HasName("PK__Programa__AF94ECA5326955EC");

                entity.ToTable("Programa");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasMany(d => d.IdMateria).WithMany(p => p.IdProgramas)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProgramaMaterium",
                        r => r.HasOne<Materia>().WithMany()
                            .HasForeignKey("IdMateria")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__ProgramaM__IdMat__534D60F1"),
                        l => l.HasOne<Programa>().WithMany()
                            .HasForeignKey("IdPrograma")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__ProgramaM__IdPro__52593CB8"),
                        j =>
                        {
                            j.HasKey("IdPrograma", "IdMateria").HasName("PK__Programa__B15598C2131A0253");
                            j.ToTable("ProgramaMateria");
                        });
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
