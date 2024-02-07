using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerceroCursoControlador : ControllerBase
    {

        private readonly PostgresContext _context;
        private readonly ILogger<TerceroCursoControlador> _logger;


        public TerceroCursoControlador( PostgresContext context, ILogger<TerceroCursoControlador> logger)
        {
                _context = context;
                _logger = logger;
            
        }


        // Get: api/TerceroCuros
        [HttpGet("GetTerceroCurso")]
        public async Task<ActionResult<IEnumerable<Terceroscursos>>> GetTerceroCurso()
        {
            try
            {

                _logger.LogInformation("Obteniendo TerceroCurso");

                var terceroCurso= await _context.Terceroscursos.ToListAsync();

                if (terceroCurso == null || !terceroCurso.Any())
                {
                    _logger.LogInformation("No se encontraron TercerosCursos");

                    return NotFound(" No se encontraron TerceroCurso");
                }

                var terceroCursoDTO = terceroCurso.Select(p => new Terceroscursos
                {

                    Id = p.Id,
                    Transaccion = p.Transaccion,
                    Tercero = p.Tercero,
                    Curso = p.Curso,
                    Fechaactivacion = p.Fechaactivacion,
                    Fechafinal  = p.Fechafinal

                }).ToList();
                _logger.LogInformation("TerceroCurso obtenidos exitosamente");
                return Ok(terceroCursoDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener terceroCurso");

                return StatusCode(500, $"Error al obtener terceroCurso: {ex.Message}");
            }


        }


        // POST: api/TerceroCurso
        [HttpPost("PostTerceroCurso")]
        public async Task<IActionResult> postTerceroCurso([FromBody] Terceroscursos terceroscurso)
        {
            try
            {
                _logger.LogInformation("Creando nuevo terceroCurso");

                // Agregar el objeto transaccioon a la colección de Transacciones en el contexto
                _context.Terceroscursos.Add(terceroscurso);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("TerceroCurso creado exitosamente");

                // Devolver un código de estado 201 (Created) para indicar que el recurso se creó correctamente
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear terceroCurso");

                // Devolver un código de estado 500 (Internal Server Error) y un mensaje de error en caso de excepción
                return StatusCode(500, $"Error al crear el terceroCurso: {ex.Message}");
            }
        }


        // PUT: api/TerceroCurso
        [HttpPut("putTerceroCurso/{id}")]
        public async Task<IActionResult> PutTerceroCurso(int id, [FromBody] Terceroscursos terceroscurso)
        {
            try
            {
                _logger.LogInformation($"Actualizando TerceroCurso con ID : {id}");

                // Verificar si el ID en el cuerpo de la solicitud coincide con el ID proporcionado en la ruta
                if (id != terceroscurso.Id)
                {
                    return BadRequest("El ID de la transaccion en el cuerpo de la solicitud no coincide con el ID proporcionado en la ruta.");
                }

                // Buscar el curso existente en la base de datos utilizando el ID
                var terceroCursoExistente = await _context.Terceroscursos.FindAsync(id);

                // Si no se encuentra la transaccion, devolver una respuesta de error NotFound
                if (terceroCursoExistente == null)
                {
                    return NotFound($"No se encontró TerceroCurso con ID: {id}");
                }

                terceroCursoExistente.Transaccion = terceroscurso.Transaccion ?? terceroCursoExistente.Transaccion;
                terceroCursoExistente.Tercero = terceroscurso.Tercero?? terceroCursoExistente.Tercero;
                terceroCursoExistente.Curso = terceroscurso.Curso ?? terceroCursoExistente.Curso;
                terceroCursoExistente.Fechaactivacion   =   terceroscurso.Fechaactivacion ?? terceroCursoExistente.Fechaactivacion;
                terceroCursoExistente.Fechafinal    =   terceroscurso.Fechafinal ?? terceroCursoExistente.Fechafinal;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                _logger.LogInformation("TerceroCurso actualizado exitosamente");

                // Devolver una respuesta sin contenido (NoContent) para indicar que la actualización fue exitosa
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar TerceroCurso");

                // Devolver una respuesta de error con un código de estado 500 (Internal Server Error) y un mensaje de error
                return StatusCode(500, $"Error al actualizar terceroCurso: {ex.Message}");
            }
        }

        // DELETE: api/TerceroCursi
        [HttpDelete("DeleteTerceroCurso/{id}")]
        public async Task<IActionResult> DeleteTerceroCurso(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando terceriCurso con ID: {id}");

                var terceroCursoExistente = await _context.Terceroscursos.SingleOrDefaultAsync(c => c.Id == id);

                if (terceroCursoExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si la transaccion no existe
                }

                _context.Terceroscursos.Remove(terceroCursoExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("terceroCurso eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Manejar excepciones de operación no válida (InvalidOperation)
                _logger.LogError(ex, "Error al eliminar terceroCurso: Operación no válida");
                return StatusCode(500, $"Error al eliminar terceroCurso: Operación no válida");
            }
            catch (Exception ex)
            {
                // Manejar excepciones genéricas
                _logger.LogError(ex, "Error al eliminar la terceroCurso");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar terceroCurso: {ex.Message}");
            }
        }

    }
}
