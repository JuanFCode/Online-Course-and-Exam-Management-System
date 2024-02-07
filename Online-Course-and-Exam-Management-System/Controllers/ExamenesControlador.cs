using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenesControlador : ControllerBase
    {

        private readonly PostgresContext _context;
        private readonly ILogger<ExamenesControlador> _logger;

        public ExamenesControlador(    PostgresContext context ,   ILogger<ExamenesControlador>   logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get: api/Examen
        [HttpGet("GetExamen")]
        public async Task<ActionResult<IEnumerable<Examenes>>> GetExamen()
        {
            try
            {

                _logger.LogInformation("Obteniendo Examen");

                var examen = await _context.Examen.ToListAsync();

                if (examen == null || !examen.Any())
                {
                    _logger.LogInformation("No se encontraron Examen");

                    return NotFound(" No se encontraron Examen");
                }

                var examenDTO = examen.Select(p => new Examenes
                {

                    Id = p.Id,
                    Curso = p.Curso,
                    Modalidad = p.Modalidad,
                    Maximopreguntas = p.Maximopreguntas,
                    Tiempomaximo = p.Tiempomaximo,
                    Porcentajerespuestas = p.Porcentajerespuestas
                   

                }).ToList();
                _logger.LogInformation("Examen obtenidos exitosamente");
                return Ok(examenDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener Examen");

                return StatusCode(500, $"Error al obtener Examen: {ex.Message}");
            }

        }

        // POST: api/Examen
        [HttpPost("PostExamen")]
        public async Task<IActionResult> PostExamen([FromBody] Examenes examan)
        {
            try
            {
                _logger.LogInformation("Creando nuevo Examen");

                // Agregar el objeto Examen a la colección de Transacciones en el contexto
                _context.Examen.Add(examan);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("Examen creado exitosamente");

                // Devolver un código de estado 201 (Created) para indicar que el recurso se creó correctamente
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Examen");

                // Devolver un código de estado 500 (Internal Server Error) y un mensaje de error en caso de excepción
                return StatusCode(500, $"Error al crear el Examen: {ex.Message}");
            }
        }

        // PUT: api/Examen
        [HttpPut("PutExamen/{id}")]
        public async Task<IActionResult> PutExamen(int id, [FromBody] Examenes examan)
        {
            try
            {
                _logger.LogInformation($"Actualizando Examen con ID : {id}");

                // Verificar si el ID en el cuerpo de la solicitud coincide con el ID proporcionado en la ruta
                if (id != examan.Id)
                {
                    return BadRequest("El ID de la transaccion en el cuerpo de la solicitud no coincide con el ID proporcionado en la ruta.");
                }

                // Buscar el curso existente en la base de datos utilizando el ID
                var examenExistente = await _context.Examen.FindAsync(id);

                // Si no se encuentra la transaccion, devolver una respuesta de error NotFound
                if (examenExistente == null)
                {
                    return NotFound($"No se encontró Examen con ID: {id}");
                }

                examenExistente.Curso = examan.Curso ?? examenExistente.Curso;
                examenExistente.Modalidad = examan.Modalidad ?? examenExistente.Modalidad;
                examenExistente.Maximopreguntas = examan.Maximopreguntas ?? examenExistente.Maximopreguntas;
                examenExistente.Tiempomaximo = examan.Tiempomaximo ?? examenExistente.Tiempomaximo;
                examenExistente.Porcentajerespuestas = examan.Porcentajerespuestas ?? examenExistente.Porcentajerespuestas;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                _logger.LogInformation("Examen actualizado exitosamente");

                // Devolver una respuesta sin contenido (NoContent) para indicar que la actualización fue exitosa
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar Examen");

                // Devolver una respuesta de error con un código de estado 500 (Internal Server Error) y un mensaje de error
                return StatusCode(500, $"Error al actualizar Examen: {ex.Message}");
            }
        }


        // DELETE: api/Examen
        [HttpDelete("DeleteExamen/{id}")]
        public async Task<IActionResult> DeleteExamen(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando Examen con ID: {id}");

                var examenExistente = await _context.Examen.SingleOrDefaultAsync(c => c.Id == id);

                if (examenExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si la transaccion no existe
                }

                _context.Examen.Remove(examenExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Examen eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Manejar excepciones de operación no válida (InvalidOperation)
                _logger.LogError(ex, "Error al eliminar Examen : Operación no válida");
                return StatusCode(500, $"Error al eliminar Examen: Operación no válida");
            }
            catch (Exception ex)
            {
                // Manejar excepciones genéricas
                _logger.LogError(ex, "Error al eliminar la Examen");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar Examen : {ex.Message}");
            }
        }

    }
}
