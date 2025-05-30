using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClinicaWeb.Models;

public partial class ClinicaDbContext : DbContext
{
    public ClinicaDbContext()
    {
    }

    public ClinicaDbContext(DbContextOptions<ClinicaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Consulta> Consultas { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<HistorialMedico> HistorialMedicos { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioMedico> UsuarioMedicos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ClinicaDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Citas__7C17FD16E2762F3B");

            entity.Property(e => e.IdCita).HasColumnName("ID_Cita");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Hora");
            entity.Property(e => e.IdMedico).HasColumnName("ID_Medico");
            entity.Property(e => e.IdPaciente).HasColumnName("ID_Paciente");
            entity.Property(e => e.MotivoConsulta)
                .HasMaxLength(250)
                .HasColumnName("Motivo_Consulta");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdMedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_Medicos");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_Pacientes");
        });

        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => e.IdConsulta).HasName("PK__Consulta__9DA76AFEC6ACE0C8");

            entity.Property(e => e.IdConsulta).HasColumnName("ID_Consulta");
            entity.Property(e => e.FechaConsulta).HasColumnName("Fecha_Consulta");
            entity.Property(e => e.IdCita).HasColumnName("ID_Cita");

            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Consultas_Citas");
        });

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Especial__3214EC27481490E9");

            entity.HasIndex(e => e.NombreEspecialidad, "UQ__Especial__448A251489E29B44").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NombreEspecialidad).HasMaxLength(100);
        });

        modelBuilder.Entity<HistorialMedico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC273CD60D9A");

            entity.ToTable("HistorialMedico");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdPaciente).HasColumnName("ID_Paciente");
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.HistorialMedicos)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialMedico_Pacientes");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medicos__3214EC2787029B86");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Medicos__531402F3F4E64704").IsUnique();

            entity.HasIndex(e => e.Dui, "UQ__Medicos__C03671B9761AD731").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido).HasMaxLength(75);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Dui)
                .HasMaxLength(20)
                .HasColumnName("DUI");
            entity.Property(e => e.IdEspecialidad).HasColumnName("ID_Especialidad");
            entity.Property(e => e.Nombre).HasMaxLength(75);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.IdEspecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medicos_Especialidades");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PACIENTE__3214EC27A38136B3");

            entity.ToTable("PACIENTES");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__PACIENTE__531402F3330947B4").IsUnique();

            entity.HasIndex(e => e.Dui, "UQ__PACIENTE__C03671B94B68D7E5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido).HasMaxLength(75);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Dui)
                .HasMaxLength(20)
                .HasColumnName("DUI");
            entity.Property(e => e.Nombre).HasMaxLength(75);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__DE4431C5F8AC85DD");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuarios__531402F3259594D1").IsUnique();

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__57A4BD192679F923").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Apellido).HasMaxLength(75);
            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(75);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("Nombre_Usuario");
            entity.Property(e => e.Rol).HasMaxLength(20);
        });

        modelBuilder.Entity<UsuarioMedico>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__UsuarioM__DE4431C540E66699");

            entity.ToTable("UsuarioMedico");

            entity.HasIndex(e => e.IdMedico, "UQ__UsuarioM__EFBF88F6F622B703").IsUnique();

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("ID_Usuario");
            entity.Property(e => e.IdMedico).HasColumnName("ID_Medico");

            entity.HasOne(d => d.IdMedicoNavigation).WithOne(p => p.UsuarioMedico)
                .HasForeignKey<UsuarioMedico>(d => d.IdMedico)
                .HasConstraintName("FK__UsuarioMe__ID_Me__74AE54BC");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.UsuarioMedico)
                .HasForeignKey<UsuarioMedico>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioMe__ID_Us__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
