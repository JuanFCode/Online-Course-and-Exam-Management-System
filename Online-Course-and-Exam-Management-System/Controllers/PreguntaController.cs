using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : ControllerBase
    {

        private readonly PostgresContext _context;
        private readonly ILogger<PreguntaController> _logger;

        public PreguntaController(PostgresContext context, ILogger<PreguntaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get: api/Pregunta
        [HttpGet("GetPregunta")]
        public async Task<ActionResult<IEnumerable<Preguntum>>> GetPregunta()
        {
            try
            {

                _logger.LogInformation("Obteniendo pregunta");

                var pregunta = await _context.Pregunta.ToListAsync();

                if (pregunta == null || !pregunta.Any())
                {
                    _logger.LogInformation("No se encontraron preguntas");

                    return NotFound(" No se encontraron preguntas");
                }

                var preguntaDTO = pregunta.Select(p => new Preguntum
                {

                    Id = p.Id,
                    Examen = p.Examen,
                    Preguntabanco = p.Preguntabanco,
                    Valortotal = p.Valortotal

                }).ToList();
                _logger.LogInformation("pregunta obtenidos exitosamente");
                return Ok(preguntaDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pregunta");

                return StatusCode(500, $"Error al obtener pregunta: {ex.Message}");
            }

        }


        // POST: api/pregunta
        [HttpPost("Postpregunta")]
        public async Task<IActionResult> Postpregunta([FromBody] Preguntum preguntum)
        {
            try
            {
                _logger.LogInformation("Creando nuevo pregunta");

                // Agregar el objeto preguntaBanco a la colección de Transacciones en el contexto
                _context.Pregunta.Add(preguntum);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("preegunta creado exitosamente");

                // Devolver un código de estado 201 (Created) para indicar que el recurso se creó correctamente
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear pregunta");

                // Devolver un código de estado 500 (Internal Server Error) y un mensaje de error en caso de excepción
                return StatusCode(500, $"Error al crear el pregunta: {ex.Message}");
            }
        }


        // PUT: api/pregunta
        [HttpPut("Putpregunta/{id}")]
        public async Task<IActionResult> Putpregunta(int id, [FromBody] Preguntum   preguntum)
        {
            try
            {
                _logger.LogInformation($"Actualizando pregunta con ID : {id}");

                // Verificar si el ID en el cuerpo de la solicitud coincide con el ID proporcionado en la ruta
                if (id != preguntum.Id)
                {
                    return BadRequest("El ID de la transaccion en el cuerpo de la solicitud no coincide con el ID proporcionado en la ruta.");
                }

                // Buscar el curso existente en la base de datos utilizando el ID
                var preguntaExistente = await _context.Pregunta.FindAsync(id);

                // Si no se encuentra la transaccion, devolver una respuesta de error NotFound
                if (preguntaExistente == null)
                {
                    return NotFound($"No se encontró pregunta con ID: {id}");
                }

                preguntaExistente.Examen = preguntum.Examen ?? preguntaExistente.Examen;
                preguntaExistente.Preguntabanco = preguntum.Preguntabanco ?? preguntaExistente.Preguntabanco;
                preguntaExistente.Valortotal = preguntum.Valortotal ?? preguntaExistente.Valortotal;



                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                _logger.LogInformation("pregunta actualizado exitosamente");

                // Devolver una respuesta sin contenido (NoContent) para indicar que la actualización fue exitosa
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar pregunta");

                // Devolver una respuesta de error con un código de estado 500 (Internal Server Error) y un mensaje de error
                return StatusCode(500, $"Error al actualizar pregunta: {ex.Message}");
            }
        }

        // DELETE: api/pregunta
        [HttpDelete("Deleteprpegunta/{id}")]
        public async Task<IActionResult> Deletepregunta(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando Pregeunta con ID: {id}");

                var preguntaExistente = await _context.Pregunta.SingleOrDefaultAsync(c => c.Id == id);

                if (preguntaExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si la transaccion no existe
                }

                _context.Pregunta.Remove(preguntaExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("pregunta eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Manejar excepciones de operación no válida (InvalidOperation)
                _logger.LogError(ex, "Error al eliminar pregunta : Operación no válida");
                return StatusCode(500, $"Error al eliminar Pregunta: Operación no válida");
            }
            catch (Exception ex)
            {
                // Manejar excepciones genéricas
                _logger.LogError(ex, "Error al eliminar la Pregunta");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar Pregunta : {ex.Message}");
            }
        }


    }
}
