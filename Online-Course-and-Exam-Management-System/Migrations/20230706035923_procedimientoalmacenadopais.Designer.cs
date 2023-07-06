﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Online_Course_and_Exam_Management_System.Models;

#nullable disable

namespace Online_Course_and_Exam_Management_System.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20230706035923_procedimientoalmacenadopais")]
    partial class procedimientoalmacenadopais
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "pg_catalog", "adminpack");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<decimal>("Costo")
                        .HasColumnType("numeric")
                        .HasColumnName("costo");

                    b.Property<string>("Descripcion")
                        .HasColumnType("character varying")
                        .HasColumnName("descripcion");

                    b.Property<DateTime?>("Duracion")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("duracion");

                    b.Property<string>("Estado")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("estado");

                    b.Property<string>("Fabricante")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("fabricante");

                    b.Property<DateTime?>("Fechadevencimiento")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("fechadevencimiento");

                    b.Property<string>("Nombre")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("nombre");

                    b.HasKey("Id")
                        .HasName("PKCURSO");

                    b.ToTable("curso", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Examan", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int?>("Curso")
                        .HasColumnType("integer")
                        .HasColumnName("curso");

                    b.Property<int?>("Maximopreguntas")
                        .HasColumnType("integer")
                        .HasColumnName("maximopreguntas");

                    b.Property<string>("Modalidad")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("modalidad");

                    b.Property<int?>("Porcentajerespuestas")
                        .HasColumnType("integer")
                        .HasColumnName("porcentajerespuestas");

                    b.Property<DateTime?>("Tiempomaximo")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("tiempomaximo");

                    b.HasKey("Id")
                        .HasName("PKEXAMEN");

                    b.HasIndex("Curso");

                    b.ToTable("examen", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Examenpresentado", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Estadoexamen")
                        .HasColumnType("character varying")
                        .HasColumnName("estadoexamen");

                    b.Property<int?>("Examen")
                        .HasColumnType("integer")
                        .HasColumnName("examen");

                    b.Property<DateTime?>("Fechafinal")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("fechafinal");

                    b.Property<DateTime?>("Fechainicio")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("fechainicio");

                    b.Property<int>("Tercero")
                        .HasColumnType("integer")
                        .HasColumnName("tercero");

                    b.Property<int?>("Ultimapreguntarespondida")
                        .HasColumnType("integer")
                        .HasColumnName("ultimapreguntarespondida");

                    b.HasKey("Id")
                        .HasName("pkexamenpresentado");

                    b.HasIndex("Examen");

                    b.HasIndex("Tercero");

                    b.ToTable("examenpresentado", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Pai", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Nombre")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("nombre");

                    b.HasKey("Id")
                        .HasName("pkpais");

                    b.ToTable("pais", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Preguntabanco", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int?>("Curso")
                        .HasColumnType("integer")
                        .HasColumnName("curso");

                    b.Property<string>("Enunciado")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("enunciado");

                    b.Property<string>("Explicacion")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("explicacion");

                    b.Property<string>("Tema")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("tema");

                    b.HasKey("Id")
                        .HasName("pkpreguntabanco");

                    b.HasIndex("Curso");

                    b.ToTable("preguntabanco", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Preguntum", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int?>("Examen")
                        .HasColumnType("integer")
                        .HasColumnName("examen");

                    b.Property<int?>("Preguntabanco")
                        .HasColumnType("integer")
                        .HasColumnName("preguntabanco");

                    b.Property<int?>("Valortotal")
                        .HasColumnType("integer")
                        .HasColumnName("valortotal");

                    b.HasKey("Id")
                        .HasName("pkpregunta");

                    b.HasIndex("Examen");

                    b.HasIndex("Preguntabanco");

                    b.ToTable("pregunta", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Respuestaexaman", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int?>("Examenpresentado")
                        .HasColumnType("integer")
                        .HasColumnName("examenpresentado");

                    b.Property<int?>("Marcada")
                        .HasColumnType("integer")
                        .HasColumnName("marcada");

                    b.Property<int?>("Preguntas")
                        .HasColumnType("integer")
                        .HasColumnName("preguntas");

                    b.Property<int?>("Respuestas")
                        .HasColumnType("integer")
                        .HasColumnName("respuestas");

                    b.Property<DateTime?>("Tiemporespuesta")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("tiemporespuesta");

                    b.HasKey("Id")
                        .HasName("respuestaexamen_pk");

                    b.HasIndex("Examenpresentado");

                    b.HasIndex("Preguntas");

                    b.HasIndex("Respuestas");

                    b.ToTable("respuestaexamen", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Respuestum", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int?>("Porcentaje")
                        .HasColumnType("integer")
                        .HasColumnName("porcentaje");

                    b.Property<int?>("Pregunta")
                        .HasColumnType("integer")
                        .HasColumnName("pregunta");

                    b.Property<int?>("Respuesta")
                        .HasColumnType("integer")
                        .HasColumnName("respuesta");

                    b.HasKey("Id")
                        .HasName("pkrespuesta");

                    b.HasIndex("Pregunta");

                    b.ToTable("respuesta", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Tercero", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Apellidos")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("apellidos");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("clave");

                    b.Property<string>("Correoelectronico")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("correoelectronico");

                    b.Property<string>("Nombre")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("nombre");

                    b.Property<int?>("Pais")
                        .HasColumnType("integer")
                        .HasColumnName("pais");

                    b.Property<string>("Tipo")
                        .HasColumnType("character varying")
                        .HasColumnName("tipo");

                    b.HasKey("Id")
                        .HasName("PKTERCERO");

                    b.HasIndex("Pais");

                    b.HasIndex(new[] { "Correoelectronico" }, "UNTERCERO")
                        .IsUnique();

                    b.ToTable("tercero", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Terceroscurso", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int?>("Curso")
                        .HasColumnType("integer")
                        .HasColumnName("curso");

                    b.Property<DateTime?>("Fechaactivacion")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("fechaactivacion");

                    b.Property<DateTime?>("Fechafinal")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("fechafinal");

                    b.Property<int?>("Tercero")
                        .HasColumnType("integer")
                        .HasColumnName("tercero");

                    b.Property<int?>("Transaccion")
                        .HasColumnType("integer")
                        .HasColumnName("transaccion");

                    b.HasKey("Id")
                        .HasName("tercerocurso_pk");

                    b.HasIndex("Curso");

                    b.HasIndex("Tercero");

                    b.HasIndex("Transaccion");

                    b.ToTable("terceroscursos", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Transaccion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Codigo")
                        .HasColumnType("character varying")
                        .HasColumnName("codigo");

                    b.Property<int?>("Cupos")
                        .HasColumnType("integer")
                        .HasColumnName("cupos");

                    b.Property<int?>("Curso")
                        .HasColumnType("integer")
                        .HasColumnName("curso");

                    b.Property<string>("Datallesadicionales")
                        .HasColumnType("character varying")
                        .HasColumnName("datallesadicionales");

                    b.Property<DateTime?>("Fechacompra")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("fechacompra");

                    b.Property<string>("Metodopago")
                        .HasColumnType("character varying")
                        .HasColumnName("metodopago");

                    b.Property<int>("Tercero")
                        .HasColumnType("integer")
                        .HasColumnName("tercero");

                    b.HasKey("Id")
                        .HasName("transaccion_pk");

                    b.HasIndex("Curso");

                    b.HasIndex("Tercero");

                    b.ToTable("transaccion", (string)null);
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Examan", b =>
                {
                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Curso", "CursoNavigation")
                        .WithMany("Examen")
                        .HasForeignKey("Curso")
                        .HasConstraintName("fkexamencurso");

                    b.Navigation("CursoNavigation");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Examenpresentado", b =>
                {
                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Examan", "ExamenNavigation")
                        .WithMany("Examenpresentados")
                        .HasForeignKey("Examen")
                        .HasConstraintName("fkexamenpresentadoexamen");

                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Tercero", "TerceroNavigation")
                        .WithMany("Examenpresentados")
                        .HasForeignKey("Tercero")
                        .IsRequired()
                        .HasConstraintName("fkexamenpresentadotercero");

                    b.Navigation("ExamenNavigation");

                    b.Navigation("TerceroNavigation");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Preguntabanco", b =>
                {
                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Curso", "CursoNavigation")
                        .WithMany("Preguntabancos")
                        .HasForeignKey("Curso")
                        .HasConstraintName("fkpreguntabancocurso");

                    b.Navigation("CursoNavigation");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Preguntum", b =>
                {
                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Examan", "ExamenNavigation")
                        .WithMany("Pregunta")
                        .HasForeignKey("Examen")
                        .HasConstraintName("fkpreguntaexamen");

                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Preguntabanco", "PreguntabancoNavigation")
                        .WithMany("Pregunta")
                        .HasForeignKey("Preguntabanco")
                        .HasConstraintName("fkpreguntapreguntabanco");

                    b.Navigation("ExamenNavigation");

                    b.Navigation("PreguntabancoNavigation");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Respuestaexaman", b =>
                {
                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Examenpresentado", "ExamenpresentadoNavigation")
                        .WithMany("Respuestaexamen")
                        .HasForeignKey("Examenpresentado")
                        .HasConstraintName("fkrespuestaexamenpresentado");

                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Preguntum", "PreguntasNavigation")
                        .WithMany("Respuestaexamen")
                        .HasForeignKey("Preguntas")
                        .HasConstraintName("fkrespuestaexamenpregunta");

                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Respuestum", "RespuestasNavigation")
                        .WithMany("Respuestaexamen")
                        .HasForeignKey("Respuestas")
                        .HasConstraintName("fkrespuestaexamenrespuesta");

                    b.Navigation("ExamenpresentadoNavigation");

                    b.Navigation("PreguntasNavigation");

                    b.Navigation("RespuestasNavigation");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Respuestum", b =>
                {
                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Preguntum", "PreguntaNavigation")
                        .WithMany("Respuesta")
                        .HasForeignKey("Pregunta")
                        .HasConstraintName("respuesta_fk");

                    b.Navigation("PreguntaNavigation");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Tercero", b =>
                {
                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Pai", "PaisNavigation")
                        .WithMany("Terceros")
                        .HasForeignKey("Pais")
                        .HasConstraintName("fkterceropais");

                    b.Navigation("PaisNavigation");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Terceroscurso", b =>
                {
                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Curso", "CursoNavigation")
                        .WithMany("Terceroscursos")
                        .HasForeignKey("Curso")
                        .HasConstraintName("terceroscursoscurso_fk");

                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Tercero", "TerceroNavigation")
                        .WithMany("Terceroscursos")
                        .HasForeignKey("Tercero")
                        .HasConstraintName("terceroscursostercero_fk");

                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Transaccion", "TransaccionNavigation")
                        .WithMany("Terceroscursos")
                        .HasForeignKey("Transaccion")
                        .HasConstraintName("tercerocurso_fk");

                    b.Navigation("CursoNavigation");

                    b.Navigation("TerceroNavigation");

                    b.Navigation("TransaccionNavigation");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Transaccion", b =>
                {
                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Curso", "CursoNavigation")
                        .WithMany("Transaccions")
                        .HasForeignKey("Curso")
                        .HasConstraintName("fktransaccioncurso");

                    b.HasOne("Online_Course_and_Exam_Management_System.Models.Tercero", "TerceroNavigation")
                        .WithMany("Transaccions")
                        .HasForeignKey("Tercero")
                        .IsRequired()
                        .HasConstraintName("fktransacciontercero");

                    b.Navigation("CursoNavigation");

                    b.Navigation("TerceroNavigation");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Curso", b =>
                {
                    b.Navigation("Examen");

                    b.Navigation("Preguntabancos");

                    b.Navigation("Terceroscursos");

                    b.Navigation("Transaccions");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Examan", b =>
                {
                    b.Navigation("Examenpresentados");

                    b.Navigation("Pregunta");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Examenpresentado", b =>
                {
                    b.Navigation("Respuestaexamen");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Pai", b =>
                {
                    b.Navigation("Terceros");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Preguntabanco", b =>
                {
                    b.Navigation("Pregunta");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Preguntum", b =>
                {
                    b.Navigation("Respuesta");

                    b.Navigation("Respuestaexamen");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Respuestum", b =>
                {
                    b.Navigation("Respuestaexamen");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Tercero", b =>
                {
                    b.Navigation("Examenpresentados");

                    b.Navigation("Terceroscursos");

                    b.Navigation("Transaccions");
                });

            modelBuilder.Entity("Online_Course_and_Exam_Management_System.Models.Transaccion", b =>
                {
                    b.Navigation("Terceroscursos");
                });
#pragma warning restore 612, 618
        }
    }
}
