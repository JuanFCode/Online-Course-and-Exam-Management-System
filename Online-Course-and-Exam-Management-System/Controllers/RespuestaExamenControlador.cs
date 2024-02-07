using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaExamenControlador : ControllerBase
    {

        private readonly PostgresContext _context;
        private readonly ILogger<RespuestaExamenControlador> _logger;

        public RespuestaExamenControlador(PostgresContext context, ILogger<RespuestaExamenControlador> logger)
        {
            _context = context;
            _logger = logger;
        }


        // Get: api/RespuestaExamen
        [HttpGet("GetRespuestaExamen")]
        public async Task<ActionResult<IEnumerable<Respuestasexamenes>>> GetRespuestaExamen()
        {
            try
            {

                _logger.LogInformation("Obteniendo RespuestaExamen");

                var respuestaExamen = await _context.Respuestaexamen.ToListAsync();

                if (respuestaExamen == null || !respuestaExamen.Any())
                {
                    _logger.LogInformation("No se encontraron respuestaExamen");

                    return NotFound(" No se encontraron respuestaExamen");
                }

                var respuestaExamenDTO = respuestaExamen.Select(p => new Respuestasexamenes
                {

                    Id = p.Id,
                    Examenpresentado = p.Examenpresentado,
                    Preguntas = p.Preguntas,
                    Respuestas = p.Respuestas,
                    Tiemporespuesta = p.Tiemporespuesta,
                    Marcada = p.Marcada,

                }).ToList();
                _logger.LogInformation("respuestaExamen obtenidos exitosamente");
                return Ok(respuestaExamenDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener respuestaExamen");

                return StatusCode(500, $"Error al obtener respuestaExamen: {ex.Message}");
            }

        }



        // POST: api/Respuesta
        [HttpPost("PostrespuestaExamen")]
        public async Task<IActionResult> Postrespuesta([FromBody] Respuestasexamenes respuestaexaman)
        {
            try
            {
                _logger.LogInformation("Creando nuevo respuestaExamen");

                // Agregar el objeto respuestaExamen a la colección de Transacciones en el contexto
                _context.Respuestaexamen.Add(respuestaexaman);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("respuestaExamen creado exitosamente");

                // Devolver un código de estado 201 (Created) para indicar que el recurso se creó correctamente
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear respuestaExamen");

                // Devolver un código de estado 500 (Internal Server Error) y un mensaje de error en caso de excepción
                return StatusCode(500, $"Error al crear el respuestaExamen: {ex.Message}");
            }
        }


        // PUT: api/respuestaExamen
        [HttpPut("PutrespuestaExamen/{id}")]
        public async Task<IActionResult> PutrespuestaExamen(int id, [FromBody] Respuestasexamenes respuestaexaman)
        {
            try
            {
                _logger.LogInformation($"Actualizando respuestaExamen con ID : {id}");

                // Verificar si el ID en el cuerpo de la solicitud coincide con el ID proporcionado en la ruta
                if (id != respuestaexaman.Id)
                {
                    return BadRequest("El ID de la transaccion en el cuerpo de la solicitud no coincide con el ID proporcionado en la ruta.");
                }

                // Buscar el curso existente en la base de datos utilizando el ID
                var respuestaExamenExistente = await _context.Respuestaexamen.FindAsync(id);

                // Si no se encuentra la transaccion, devolver una respuesta de error NotFound
                if (respuestaExamenExistente == null)
                {
                    return NotFound($"No se encontró respuesta con ID: {id}");
                }

                respuestaExamenExistente.Examenpresentado = respuestaexaman.Examenpresentado ?? respuestaExamenExistente.Examenpresentado;
                respuestaExamenExistente.Preguntas = respuestaexaman.Preguntas ?? respuestaExamenExistente.Preguntas;
                respuestaExamenExistente.Respuestas = respuestaexaman.Respuestas ?? respuestaExamenExistente.Respuestas;
                respuestaExamenExistente.Tiemporespuesta = respuestaexaman.Tiemporespuesta ?? respuestaExamenExistente.Tiemporespuesta;
                respuestaExamenExistente.Marcada = respuestaexaman.Marcada ?? respuestaExamenExistente.Marcada;



                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                _logger.LogInformation("respuestaExamen actualizado exitosamente");

                // Devolver una respuesta sin contenido (NoContent) para indicar que la actualización fue exitosa
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar respuestaExamen");

                // Devolver una respuesta de error con un código de estado 500 (Internal Server Error) y un mensaje de error
                return StatusCode(500, $"Error al actualizar respuestaExamen: {ex.Message}");
            }
        }



        // DELETE: api/respuestaExamen
        [HttpDelete("DeleterespuestaExamen/{id}")]
        public async Task<IActionResult> DeleterespuestaExamens(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando respuestaExamen con ID: {id}");

                var respuestaExamenExistente = await _context.Respuestaexamen.SingleOrDefaultAsync(c => c.Id == id);

                if (respuestaExamenExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si la transaccion no existe
                }

                _context.Respuestaexamen.Remove(respuestaExamenExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("respuestaExamen eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Manejar excepciones de operación no válida (InvalidOperation)
                _logger.LogError(ex, "Error al eliminar respuestaExamen : Operación no válida");
                return StatusCode(500, $"Error al eliminar respuestaExamen: Operación no válida");
            }
            catch (Exception ex)
            {
                // Manejar excepciones genéricas
                _logger.LogError(ex, "Error al eliminar la respuestaExamen");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar respuestaExamen : {ex.Message}");
            }
        }


    }
}
