using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<RespuestaController> _logger;

        public RespuestaController(PostgresContext context, ILogger<RespuestaController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // Get: api/Respuesta
        [HttpGet("GetRespuesta")]
        public async Task<ActionResult<IEnumerable<Respuestum>>> GetRespuesta()
        {
            try
            {

                _logger.LogInformation("Obteniendo respusta");

                var respuesta = await _context.Respuesta.ToListAsync();

                if (respuesta == null || !respuesta.Any())
                {
                    _logger.LogInformation("No se encontraron respuestas");

                    return NotFound(" No se encontraron respuestas");
                }

                var respuestaDTO = respuesta.Select(p => new Respuestum
                {

                    Id = p.Id,
                    Respuesta = p.Respuesta,
                    Pregunta = p.Pregunta,
                    Porcentaje = p.Porcentaje

                }).ToList();
                _logger.LogInformation("respuesta obtenidos exitosamente");
                return Ok(respuestaDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener respuesta");

                return StatusCode(500, $"Error al obtener respuesta: {ex.Message}");
            }

        }

        // POST: api/Respuesta
        [HttpPost("PostRespuesta")]
        public async Task<IActionResult> Postrespuesta([FromBody] Respuestum   respuestum)
        {
            try
            {
                _logger.LogInformation("Creando nuevo respuesta");

                // Agregar el objeto respuesta a la colección de Transacciones en el contexto
                _context.Respuesta.Add(respuestum);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("respuesta creado exitosamente");

                // Devolver un código de estado 201 (Created) para indicar que el recurso se creó correctamente
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear respuesta");

                // Devolver un código de estado 500 (Internal Server Error) y un mensaje de error en caso de excepción
                return StatusCode(500, $"Error al crear el pregunta: {ex.Message}");
            }
        }


        // PUT: api/respuesta
        [HttpPut("PutRespuesta/{id}")]
        public async Task<IActionResult> Putrespuesta(int id, [FromBody] Respuestum respuestum)
        {
            try
            {
                _logger.LogInformation($"Actualizando respuesta con ID : {id}");

                // Verificar si el ID en el cuerpo de la solicitud coincide con el ID proporcionado en la ruta
                if (id != respuestum.Id)
                {
                    return BadRequest("El ID de la transaccion en el cuerpo de la solicitud no coincide con el ID proporcionado en la ruta.");
                }

                // Buscar el curso existente en la base de datos utilizando el ID
                var respuestaExistente = await _context.Respuesta.FindAsync(id);

                // Si no se encuentra la transaccion, devolver una respuesta de error NotFound
                if (respuestaExistente == null)
                {
                    return NotFound($"No se encontró respuesta con ID: {id}");
                }

                respuestaExistente.Respuesta = respuestum.Respuesta ?? respuestaExistente.Respuesta;
                respuestaExistente.Pregunta = respuestum.Pregunta ?? respuestaExistente.Pregunta;
                respuestaExistente.Porcentaje = respuestum.Porcentaje ?? respuestaExistente.Porcentaje;


                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                _logger.LogInformation("respuesta actualizado exitosamente");

                // Devolver una respuesta sin contenido (NoContent) para indicar que la actualización fue exitosa
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar respuesta");

                // Devolver una respuesta de error con un código de estado 500 (Internal Server Error) y un mensaje de error
                return StatusCode(500, $"Error al actualizar respuesta: {ex.Message}");
            }
        }


        // DELETE: api/respuesta
        [HttpDelete("Deleterespuesta/{id}")]
        public async Task<IActionResult> Deleterespuesta(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando respuesta con ID: {id}");

                var respuestaExistente = await _context.Respuesta.SingleOrDefaultAsync(c => c.Id == id);

                if (respuestaExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si la transaccion no existe
                }

                _context.Respuesta.Remove(respuestaExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("respuesta eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Manejar excepciones de operación no válida (InvalidOperation)
                _logger.LogError(ex, "Error al eliminar respuesta : Operación no válida");
                return StatusCode(500, $"Error al eliminar respuesta: Operación no válida");
            }
            catch (Exception ex)
            {
                // Manejar excepciones genéricas
                _logger.LogError(ex, "Error al eliminar la respuesta");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar respuesta : {ex.Message}");
            }
        }


    }
}
