using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Online_Course_and_Exam_Management_System.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Examan> Examen { get; set; }

    public virtual DbSet<Examenpresentado> Examenpresentados { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Preguntabanco> Preguntabancos { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Respuestaexaman> Respuestaexamen { get; set; }

    public virtual DbSet<Respuestum> Respuesta { get; set; }

    public virtual DbSet<Tercero> Terceros { get; set; }

    public virtual DbSet<Terceroscurso> Terceroscursos { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PKCURSO");

            entity.ToTable("curso");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Duracion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("duracion");
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .HasColumnName("estado");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(255)
                .HasColumnName("fabricante");
            entity.Property(e => e.Fechadevencimiento)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechadevencimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Examan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PKEXAMEN");

            entity.ToTable("examen");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Curso).HasColumnName("curso");
            entity.Property(e => e.Maximopreguntas).HasColumnName("maximopreguntas");
            entity.Property(e => e.Modalidad)
                .HasMaxLength(255)
                .HasColumnName("modalidad");
            entity.Property(e => e.Porcentajerespuestas).HasColumnName("porcentajerespuestas");
            entity.Property(e => e.Tiempomaximo)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tiempomaximo");

            entity.HasOne(d => d.CursoNavigation).WithMany(p => p.Examen)
                .HasForeignKey(d => d.Curso)
                .HasConstraintName("fkexamencurso");
        });

        modelBuilder.Entity<Examenpresentado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkexamenpresentado");

            entity.ToTable("examenpresentado");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Estadoexamen)
                .HasColumnType("character varying")
                .HasColumnName("estadoexamen");
            entity.Property(e => e.Examen).HasColumnName("examen");
            entity.Property(e => e.Fechafinal)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechafinal");
            entity.Property(e => e.Fechainicio)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechainicio");
            entity.Property(e => e.Tercero).HasColumnName("tercero");
            entity.Property(e => e.Ultimapreguntarespondida).HasColumnName("ultimapreguntarespondida");

            entity.HasOne(d => d.ExamenNavigation).WithMany(p => p.Examenpresentados)
                .HasForeignKey(d => d.Examen)
                .HasConstraintName("fkexamenpresentadoexamen");

            entity.HasOne(d => d.TerceroNavigation).WithMany(p => p.Examenpresentados)
                .HasForeignKey(d => d.Tercero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkexamenpresentadotercero");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkpais");

            entity.ToTable("pais");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Preguntabanco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkpreguntabanco");

            entity.ToTable("preguntabanco");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Curso).HasColumnName("curso");
            entity.Property(e => e.Enunciado)
                .HasMaxLength(2000)
                .HasColumnName("enunciado");
            entity.Property(e => e.Explicacion)
                .HasMaxLength(2000)
                .HasColumnName("explicacion");
            entity.Property(e => e.Tema)
                .HasMaxLength(255)
                .HasColumnName("tema");

            entity.HasOne(d => d.CursoNavigation).WithMany(p => p.Preguntabancos)
                .HasForeignKey(d => d.Curso)
                .HasConstraintName("fkpreguntabancocurso");
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkpregunta");

            entity.ToTable("pregunta");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Examen).HasColumnName("examen");
            entity.Property(e => e.Preguntabanco).HasColumnName("preguntabanco");
            entity.Property(e => e.Valortotal).HasColumnName("valortotal");

            entity.HasOne(d => d.ExamenNavigation).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.Examen)
                .HasConstraintName("fkpreguntaexamen");

            entity.HasOne(d => d.PreguntabancoNavigation).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.Preguntabanco)
                .HasConstraintName("fkpreguntapreguntabanco");
        });

        modelBuilder.Entity<Respuestaexaman>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("respuestaexamen_pk");

            entity.ToTable("respuestaexamen");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Examenpresentado).HasColumnName("examenpresentado");
            entity.Property(e => e.Marcada).HasColumnName("marcada");
            entity.Property(e => e.Preguntas).HasColumnName("preguntas");
            entity.Property(e => e.Respuestas).HasColumnName("respuestas");
            entity.Property(e => e.Tiemporespuesta)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tiemporespuesta");

            entity.HasOne(d => d.ExamenpresentadoNavigation).WithMany(p => p.Respuestaexamen)
                .HasForeignKey(d => d.Examenpresentado)
                .HasConstraintName("fkrespuestaexamenpresentado");

            entity.HasOne(d => d.PreguntasNavigation).WithMany(p => p.Respuestaexamen)
                .HasForeignKey(d => d.Preguntas)
                .HasConstraintName("fkrespuestaexamenpregunta");

            entity.HasOne(d => d.RespuestasNavigation).WithMany(p => p.Respuestaexamen)
                .HasForeignKey(d => d.Respuestas)
                .HasConstraintName("fkrespuestaexamenrespuesta");
        });

        modelBuilder.Entity<Respuestum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkrespuesta");

            entity.ToTable("respuesta");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");
            entity.Property(e => e.Pregunta).HasColumnName("pregunta");
            entity.Property(e => e.Respuesta).HasColumnName("respuesta");

            entity.HasOne(d => d.PreguntaNavigation).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.Pregunta)
                .HasConstraintName("respuesta_fk");
        });

        modelBuilder.Entity<Tercero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PKTERCERO");

            entity.ToTable("tercero");

            entity.HasIndex(e => e.Correoelectronico, "UNTERCERO").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(255)
                .HasColumnName("apellidos");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .HasColumnName("clave");
            entity.Property(e => e.Correoelectronico)
                .HasMaxLength(255)
                .HasColumnName("correoelectronico");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais).HasColumnName("pais");
            entity.Property(e => e.Tipo)
                .HasColumnType("character varying")
                .HasColumnName("tipo");

            entity.HasOne(d => d.PaisNavigation).WithMany(p => p.Terceros)
                .HasForeignKey(d => d.Pais)
                .HasConstraintName("fkterceropais");
        });

        modelBuilder.Entity<Terceroscurso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tercerocurso_pk");

            entity.ToTable("terceroscursos");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Curso).HasColumnName("curso");
            entity.Property(e => e.Fechaactivacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaactivacion");
            entity.Property(e => e.Fechafinal)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechafinal");
            entity.Property(e => e.Tercero).HasColumnName("tercero");
            entity.Property(e => e.Transaccion).HasColumnName("transaccion");

            entity.HasOne(d => d.CursoNavigation).WithMany(p => p.Terceroscursos)
                .HasForeignKey(d => d.Curso)
                .HasConstraintName("terceroscursoscurso_fk");

            entity.HasOne(d => d.TerceroNavigation).WithMany(p => p.Terceroscursos)
                .HasForeignKey(d => d.Tercero)
                .HasConstraintName("terceroscursostercero_fk");

            entity.HasOne(d => d.TransaccionNavigation).WithMany(p => p.Terceroscursos)
                .HasForeignKey(d => d.Transaccion)
                .HasConstraintName("tercerocurso_fk");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaccion_pk");

            entity.ToTable("transaccion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasColumnType("character varying")
                .HasColumnName("codigo");
            entity.Property(e => e.Cupos).HasColumnName("cupos");
            entity.Property(e => e.Curso).HasColumnName("curso");
            entity.Property(e => e.Datallesadicionales)
                .HasColumnType("character varying")
                .HasColumnName("datallesadicionales");
            entity.Property(e => e.Fechacompra)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacompra");
            entity.Property(e => e.Metodopago)
                .HasColumnType("character varying")
                .HasColumnName("metodopago");
            entity.Property(e => e.Tercero).HasColumnName("tercero");

            entity.HasOne(d => d.CursoNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.Curso)
                .HasConstraintName("fktransaccioncurso");

            entity.HasOne(d => d.TerceroNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.Tercero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fktransacciontercero");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
