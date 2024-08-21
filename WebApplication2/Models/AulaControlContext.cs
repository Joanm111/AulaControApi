using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class AulaControlContext : DbContext
{
    public AulaControlContext()
    {
    }

    public AulaControlContext(DbContextOptions<AulaControlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<Calificacion> Calificacions { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ComentarioForo> ComentarioForos { get; set; }

    public virtual DbSet<Conductum> Conducta { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Foro> Foros { get; set; }

    public virtual DbSet<Grado> Grados { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    public virtual DbSet<MensajeChat> MensajeChats { get; set; }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<Padre> Padres { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Meregildo;Initial Catalog=AulaControl;User ID=joan;Password=123456;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asistenc__3214EC27156490F7");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__Asistenci__Estud__5AB9788F");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Califica__3214EC27931A126D");

            entity.ToTable("Calificacion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Calificacions)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__Calificac__Estud__540C7B00");

            entity.HasOne(d => d.Materia).WithMany(p => p.Calificacions)
                .HasForeignKey(d => d.MateriaId)
                .HasConstraintName("FK__Calificac__Mater__55009F39");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Chat__3214EC27B144F276");

            entity.ToTable("Chat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PadreId).HasColumnName("PadreID");
            entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

            entity.HasOne(d => d.Padre).WithMany(p => p.Chats)
                .HasForeignKey(d => d.PadreId)
                .HasConstraintName("FK__Chat__PadreID__690797E6");

            entity.HasOne(d => d.Profesor).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ProfesorId)
                .HasConstraintName("FK__Chat__ProfesorID__681373AD");
        });

        modelBuilder.Entity<ComentarioForo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3214EC2755504271");

            entity.ToTable("ComentarioForo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comentario).HasMaxLength(1000);
            entity.Property(e => e.ForoId).HasColumnName("ForoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Foro).WithMany(p => p.ComentarioForos)
                .HasForeignKey(d => d.ForoId)
                .HasConstraintName("FK__Comentari__ForoI__6442E2C9");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ComentarioForos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Comentari__Usuar__65370702");
        });

        modelBuilder.Entity<Conductum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conducta__3214EC27743580EB");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Conducta)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__Conducta__Estudi__57DD0BE4");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estudian__3214EC270F3FD31A");

            entity.ToTable("Estudiante");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.GradoId).HasColumnName("GradoID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PadreId).HasColumnName("PadreID");

            entity.HasOne(d => d.Grado).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.GradoId)
                .HasConstraintName("FK__Estudiant__Grado__51300E55");

            entity.HasOne(d => d.Padre).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.PadreId)
                .HasConstraintName("FK__Estudiant__Padre__503BEA1C");
        });

        modelBuilder.Entity<Foro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Foro__3214EC278453C6C0");

            entity.ToTable("Foro");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.GradoId).HasColumnName("GradoID");
            entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");
            entity.Property(e => e.Titulo).HasMaxLength(255);

            entity.HasOne(d => d.Grado).WithMany(p => p.Foros)
                .HasForeignKey(d => d.GradoId)
                .HasConstraintName("FK__Foro__GradoID__607251E5");

            entity.HasOne(d => d.Profesor).WithMany(p => p.Foros)
                .HasForeignKey(d => d.ProfesorId)
                .HasConstraintName("FK__Foro__ProfesorID__6166761E");
        });

        modelBuilder.Entity<Grado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grado__3214EC2791C1971B");

            entity.ToTable("Grado");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Materia__3214EC273FAD923D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

            entity.HasOne(d => d.Profesor).WithMany(p => p.Materia)
                .HasForeignKey(d => d.ProfesorId)
                .HasConstraintName("FK__Materia__Profeso__4A8310C6");
        });

        modelBuilder.Entity<MensajeChat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MensajeC__3214EC27680EA862");

            entity.ToTable("MensajeChat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChatId).HasColumnName("ChatID");
            entity.Property(e => e.Mensaje).HasMaxLength(1000);
            entity.Property(e => e.RemitenteId).HasColumnName("RemitenteID");

            entity.HasOne(d => d.Chat).WithMany(p => p.MensajeChats)
                .HasForeignKey(d => d.ChatId)
                .HasConstraintName("FK__MensajeCh__ChatI__6CD828CA");

            entity.HasOne(d => d.Remitente).WithMany(p => p.MensajeChats)
                .HasForeignKey(d => d.RemitenteId)
                .HasConstraintName("FK__MensajeCh__Remit__6DCC4D03");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC27B07D03AE");

            entity.ToTable("Notificacion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Tipo).HasMaxLength(50);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Notificac__Usuar__719CDDE7");
        });

        modelBuilder.Entity<Padre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Padre__3214EC272B86239C");

            entity.ToTable("Padre");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Padres)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Padre__UsuarioID__4D5F7D71");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profesor__3214EC278169CDD9");

            entity.ToTable("Profesor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.GradoId).HasColumnName("GradoID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Grado).WithMany(p => p.Profesors)
                .HasForeignKey(d => d.GradoId)
                .HasConstraintName("FK__Profesor__GradoI__47A6A41B");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Profesors)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Profesor__Usuari__46B27FE2");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reporte__3214EC27601CEC75");

            entity.ToTable("Reporte");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__Reporte__Estudia__5D95E53A");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3214EC2736E694F2");

            entity.ToTable("Rol");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC27A33821AB");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(255);
            entity.Property(e => e.RolId).HasColumnName("RolID");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuario__RolID__41EDCAC5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
