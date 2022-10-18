using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TDS.Models
{
    public partial class TDSContext : DbContext
    {
        public TDSContext()
        {
        }

        public TDSContext(DbContextOptions<TDSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clase> Clases { get; set; } = null!;
        public virtual DbSet<Entrega> Entregas { get; set; } = null!;
        public virtual DbSet<Estudiante> Estudiantes { get; set; } = null!;
        public virtual DbSet<EstudiantesClase> EstudiantesClases { get; set; } = null!;
        public virtual DbSet<Institucion> Institucions { get; set; } = null!;
        public virtual DbSet<Maestro> Maestros { get; set; } = null!;
        public virtual DbSet<Mensaje> Mensajes { get; set; } = null!;
        public virtual DbSet<MensajesDetalle> MensajesDetalles { get; set; } = null!;
        public virtual DbSet<Tarea> Tareas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost; Database=TDS; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clase>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Institucion)
                    .WithMany(p => p.Clases)
                    .HasForeignKey(d => d.InstitucionId)
                    .HasConstraintName("FK__Clases__Instituc__2A4B4B5E");

                entity.HasOne(d => d.Maestro)
                    .WithMany(p => p.Clases)
                    .HasForeignKey(d => d.MaestroId)
                    .HasConstraintName("FK__Clases__MaestroI__29572725");
            });

            modelBuilder.Entity<Entrega>(entity =>
            {
                entity.ToTable("Entrega");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Documento).HasColumnType("text");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.Entregas)
                    .HasForeignKey(d => d.EstudianteId)
                    .HasConstraintName("FK__Entrega__Estudia__4AB81AF0");

                entity.HasOne(d => d.Tarea)
                    .WithMany(p => p.Entregas)
                    .HasForeignKey(d => d.TareaId)
                    .HasConstraintName("FK__Entrega__TareaId__49C3F6B7");
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Institucion)
                    .WithMany(p => p.Estudiantes)
                    .HasForeignKey(d => d.InstitucionId)
                    .HasConstraintName("FK_Institucion");
            });

            modelBuilder.Entity<EstudiantesClase>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Clase)
                    .WithMany(p => p.EstudiantesClases)
                    .HasForeignKey(d => d.ClaseId)
                    .HasConstraintName("FK__Estudiant__Clase__36B12243");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.EstudiantesClases)
                    .HasForeignKey(d => d.EstudianteId)
                    .HasConstraintName("FK__Estudiant__Estud__35BCFE0A");
            });

            modelBuilder.Entity<Institucion>(entity =>
            {
                entity.ToTable("Institucion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Maestro>(entity =>
            {
                entity.ToTable("Maestro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Correo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Institucion)
                    .WithMany(p => p.Maestros)
                    .HasForeignKey(d => d.InstitucionId)
                    .HasConstraintName("FK__Maestro__Institu__267ABA7A");
            });

            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Texto)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Clase)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.ClaseId)
                    .HasConstraintName("FK__Mensajes__ClaseI__5BE2A6F2");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.EstudianteId)
                    .HasConstraintName("FK__Mensajes__Estudi__3A81B327");
            });

            modelBuilder.Entity<MensajesDetalle>(entity =>
            {
                entity.ToTable("MensajesDetalle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Texto)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Clase)
                    .WithMany(p => p.MensajesDetalles)
                    .HasForeignKey(d => d.ClaseId)
                    .HasConstraintName("FK__MensajesD__Clase__5AEE82B9");

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.MensajesDetalles)
                    .HasForeignKey(d => d.EstudianteId)
                    .HasConstraintName("FK__MensajesD__Estud__4316F928");

                entity.HasOne(d => d.Mensaje)
                    .WithMany(p => p.MensajesDetalles)
                    .HasForeignKey(d => d.MensajeId)
                    .HasConstraintName("FK__MensajesD__Mensa__440B1D61");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.ToTable("Tarea");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.FechaEntrega).HasColumnType("datetime");

                entity.Property(e => e.IdClase).HasColumnName("idClase");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClaseNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdClase)
                    .HasConstraintName("FK__Tarea__idClase__46E78A0C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
