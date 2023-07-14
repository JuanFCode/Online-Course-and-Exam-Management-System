using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenPresentadoController : ControllerBase
    {

        private readonly PostgresContext _context;
        private readonly ILogger<ExamenPresentadoController> _logger;

        public ExamenPresentadoController(PostgresContext context, ILogger<ExamenPresentadoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get: api/Respuesta
        [HttpGet("GetExamenPresentado")]
        public async Task<ActionResult<IEnumerable<Examenpresentado>>> GetExamenPresentado()
        {
            try
            {

                _logger.LogInformation("Obteniendo ExamenPresentado");

                var examenPresentado = await _context.Examenpresentados.ToListAsync();

                if (examenPresentado == null || !examenPresentado.Any())
                {
                    _logger.LogInformation("No se encontraron Examenespresentados");

                    return NotFound(" No se encontraron Examenespresentados ");
                }

                var examenPresentadoDTO = examenPresentado.Select(p => new Examenpresentado
                {

                    Id = p.Id,
                    Tercero = p.Tercero,
                    Examen = p.Examen,
                    Fechainicio = p.Fechainicio,
                    Fechafinal = p.Fechafinal,
                    Ultimapreguntarespondida = p.Ultimapreguntarespondida,
                    Estadoexamen = p.Estadoexamen

                }).ToList();
                _logger.LogInformation("Examenespresentados obtenidos exitosamente");
                return Ok(examenPresentadoDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener Examenespresentados");

                return StatusCode(500, $"Error al obtener Examenespresentados: {ex.Message}");
            }

        }


        // POST: api/ExamenPresentado
        [HttpPost("PostExamenPresentado")]
        public async Task<IActionResult> PostExamenPresentado([FromBody] Examenpresentado    examenpresentado)
        {
            try
            {
                _logger.LogInformation("Creando nuevo ExamenPresentado");

                // Agregar el objeto ExamenPresentado a la colección de Transacciones en el contexto
                _context.Examenpresentados.Add(examenpresentado);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("ExamenPresentado creado exitosamente");

                // Devolver un código de estado 201 (Created) para indicar que el recurso se creó correctamente
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear ExamenPresentado");

                // Devolver un código de estado 500 (Internal Server Error) y un mensaje de error en caso de excepción
                return StatusCode(500, $"Error al crear el ExamenPresentado: {ex.Message}");
            }
        }


    }
}
