using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Course_and_Exam_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ProcedimientoAlmacenado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_catalog.adminpack", ",,");

            migrationBuilder.CreateTable(
                name: "curso",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    fabricante = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    fechadevencimiento = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    estado = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    costo = table.Column<decimal>(type: "numeric", nullable: false),
                    duracion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    descripcion = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKCURSO", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pais",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkpais", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "examen",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    curso = table.Column<int>(type: "integer", nullable: true),
                    modalidad = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    maximopreguntas = table.Column<int>(type: "integer", nullable: true),
                    tiempomaximo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    porcentajerespuestas = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKEXAMEN", x => x.id);
                    table.ForeignKey(
                        name: "fkexamencurso",
                        column: x => x.curso,
                        principalTable: "curso",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "preguntabanco",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    curso = table.Column<int>(type: "integer", nullable: true),
                    tema = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    enunciado = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    explicacion = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkpreguntabanco", x => x.id);
                    table.ForeignKey(
                        name: "fkpreguntabancocurso",
                        column: x => x.curso,
                        principalTable: "curso",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tercero",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    apellidos = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    pais = table.Column<int>(type: "integer", nullable: true),
                    correoelectronico = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    clave = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tipo = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKTERCERO", x => x.id);
                    table.ForeignKey(
                        name: "fkterceropais",
                        column: x => x.pais,
                        principalTable: "pais",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "pregunta",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    examen = table.Column<int>(type: "integer", nullable: true),
                    preguntabanco = table.Column<int>(type: "integer", nullable: true),
                    valortotal = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkpregunta", x => x.id);
                    table.ForeignKey(
                        name: "fkpreguntaexamen",
                        column: x => x.examen,
                        principalTable: "examen",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkpreguntapreguntabanco",
                        column: x => x.preguntabanco,
                        principalTable: "preguntabanco",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "examenpresentado",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    tercero = table.Column<int>(type: "integer", nullable: false),
                    examen = table.Column<int>(type: "integer", nullable: true),
                    fechainicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    fechafinal = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ultimapreguntarespondida = table.Column<int>(type: "integer", nullable: true),
                    estadoexamen = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkexamenpresentado", x => x.id);
                    table.ForeignKey(
                        name: "fkexamenpresentadoexamen",
                        column: x => x.examen,
                        principalTable: "examen",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkexamenpresentadotercero",
                        column: x => x.tercero,
                        principalTable: "tercero",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "transaccion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    tercero = table.Column<int>(type: "integer", nullable: false),
                    curso = table.Column<int>(type: "integer", nullable: true),
                    fechacompra = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    metodopago = table.Column<string>(type: "character varying", nullable: true),
                    datallesadicionales = table.Column<string>(type: "character varying", nullable: true),
                    cupos = table.Column<int>(type: "integer", nullable: true),
                    codigo = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transaccion_pk", x => x.id);
                    table.ForeignKey(
                        name: "fktransaccioncurso",
                        column: x => x.curso,
                        principalTable: "curso",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fktransacciontercero",
                        column: x => x.tercero,
                        principalTable: "tercero",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "respuesta",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    respuesta = table.Column<int>(type: "integer", nullable: true),
                    pregunta = table.Column<int>(type: "integer", nullable: true),
                    porcentaje = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkrespuesta", x => x.id);
                    table.ForeignKey(
                        name: "respuesta_fk",
                        column: x => x.pregunta,
                        principalTable: "pregunta",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "terceroscursos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    transaccion = table.Column<int>(type: "integer", nullable: true),
                    tercero = table.Column<int>(type: "integer", nullable: true),
                    curso = table.Column<int>(type: "integer", nullable: true),
                    fechaactivacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    fechafinal = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tercerocurso_pk", x => x.id);
                    table.ForeignKey(
                        name: "tercerocurso_fk",
                        column: x => x.transaccion,
                        principalTable: "transaccion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "terceroscursoscurso_fk",
                        column: x => x.curso,
                        principalTable: "curso",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "terceroscursostercero_fk",
                        column: x => x.tercero,
                        principalTable: "tercero",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "respuestaexamen",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    examenpresentado = table.Column<int>(type: "integer", nullable: true),
                    preguntas = table.Column<int>(type: "integer", nullable: true),
                    respuestas = table.Column<int>(type: "integer", nullable: true),
                    tiemporespuesta = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    marcada = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("respuestaexamen_pk", x => x.id);
                    table.ForeignKey(
                        name: "fkrespuestaexamenpregunta",
                        column: x => x.preguntas,
                        principalTable: "pregunta",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkrespuestaexamenpresentado",
                        column: x => x.examenpresentado,
                        principalTable: "examenpresentado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkrespuestaexamenrespuesta",
                        column: x => x.respuestas,
                        principalTable: "respuesta",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_examen_curso",
                table: "examen",
                column: "curso");

            migrationBuilder.CreateIndex(
                name: "IX_examenpresentado_examen",
                table: "examenpresentado",
                column: "examen");

            migrationBuilder.CreateIndex(
                name: "IX_examenpresentado_tercero",
                table: "examenpresentado",
                column: "tercero");

            migrationBuilder.CreateIndex(
                name: "IX_pregunta_examen",
                table: "pregunta",
                column: "examen");

            migrationBuilder.CreateIndex(
                name: "IX_pregunta_preguntabanco",
                table: "pregunta",
                column: "preguntabanco");

            migrationBuilder.CreateIndex(
                name: "IX_preguntabanco_curso",
                table: "preguntabanco",
                column: "curso");

            migrationBuilder.CreateIndex(
                name: "IX_respuesta_pregunta",
                table: "respuesta",
                column: "pregunta");

            migrationBuilder.CreateIndex(
                name: "IX_respuestaexamen_examenpresentado",
                table: "respuestaexamen",
                column: "examenpresentado");

            migrationBuilder.CreateIndex(
                name: "IX_respuestaexamen_preguntas",
                table: "respuestaexamen",
                column: "preguntas");

            migrationBuilder.CreateIndex(
                name: "IX_respuestaexamen_respuestas",
                table: "respuestaexamen",
                column: "respuestas");

            migrationBuilder.CreateIndex(
                name: "IX_tercero_pais",
                table: "tercero",
                column: "pais");

            migrationBuilder.CreateIndex(
                name: "UNTERCERO",
                table: "tercero",
                column: "correoelectronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_terceroscursos_curso",
                table: "terceroscursos",
                column: "curso");

            migrationBuilder.CreateIndex(
                name: "IX_terceroscursos_tercero",
                table: "terceroscursos",
                column: "tercero");

            migrationBuilder.CreateIndex(
                name: "IX_terceroscursos_transaccion",
                table: "terceroscursos",
                column: "transaccion");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_curso",
                table: "transaccion",
                column: "curso");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_tercero",
                table: "transaccion",
                column: "tercero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "respuestaexamen");

            migrationBuilder.DropTable(
                name: "terceroscursos");

            migrationBuilder.DropTable(
                name: "examenpresentado");

            migrationBuilder.DropTable(
                name: "respuesta");

            migrationBuilder.DropTable(
                name: "transaccion");

            migrationBuilder.DropTable(
                name: "pregunta");

            migrationBuilder.DropTable(
                name: "tercero");

            migrationBuilder.DropTable(
                name: "examen");

            migrationBuilder.DropTable(
                name: "preguntabanco");

            migrationBuilder.DropTable(
                name: "pais");

            migrationBuilder.DropTable(
                name: "curso");
        }
    }
}
