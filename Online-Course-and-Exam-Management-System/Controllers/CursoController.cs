using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<CursoController> _logger;

        public CursoController(PostgresContext context, ILogger<CursoController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // Obtiene la lista de los cursos
        [HttpGet("GetCurso")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCurso()
        {
            try
            {
                _logger.LogInformation("Obteniendo cursos");

                var cursos = await _context.Cursos.ToListAsync();

                if (cursos == null || !cursos.Any())
                {
                    _logger.LogInformation("No se encontraron cursos");
                    return NotFound("No se encontraron cursos.");
                }

                var cursoDTO = cursos.Select(p => new Curso
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Fabricante = p.Fabricante,
                    Fechadevencimiento = p.Fechadevencimiento,
                    Estado = p.Estado,
                    Costo = p.Costo,
                    Duracion = p.Duracion,
                    Descripcion = p.Descripcion
                }).ToList();

                _logger.LogInformation("Cursos obtenidos exitosamente");

                return Ok(cursoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los cursos");

                return StatusCode(500, $"Error al obtener los cursos: {ex.Message}");
            }
        }

        [HttpPost("PostCurso")]
        public async Task<IActionResult> CreateCurso([FromBody] Curso curso)
        {
            try
            {
                _logger.LogInformation("Creando nuevo curso");

                // Agregar el objeto curso a la colección de Cursos en el contexto
                _context.Cursos.Add(curso);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("Curso creado exitosamente");

                // Devolver un código de estado 201 (Created) para indicar que el recurso se creó correctamente
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el curso");

                // Devolver un código de estado 500 (Internal Server Error) y un mensaje de error en caso de excepción
                return StatusCode(500, $"Error al crear el curso: {ex.Message}");
            }
        }

        [HttpPut("putCurso/{id}")]
        public async Task<IActionResult> PutCurso(int id, [FromBody] Curso curso)
        {
            try
            {
                _logger.LogInformation($"Actualizando Curso con ID: {id}");

                // Verificar si el ID en el cuerpo de la solicitud coincide con el ID proporcionado en la ruta
                if (id != curso.Id)
                {
                    return BadRequest("El ID del curso en el cuerpo de la solicitud no coincide con el ID proporcionado en la ruta.");
                }

                // Buscar el curso existente en la base de datos utilizando el ID
                var cursoExistente = await _context.Cursos.FindAsync(id);

                // Si no se encuentra el curso, devolver una respuesta de error NotFound
                if (cursoExistente == null)
                {
                    return NotFound($"No se encontró el curso con ID: {id}");
                }

                // Actualizar los campos individuales del curso existente solo si han sido modificados
                cursoExistente.Nombre = curso.Nombre ?? cursoExistente.Nombre;
                cursoExistente.Fabricante = curso.Fabricante ?? cursoExistente.Fabricante;
                cursoExistente.Fechadevencimiento = curso.Fechadevencimiento ?? cursoExistente.Fechadevencimiento;
                cursoExistente.Estado = curso.Estado ?? cursoExistente.Estado;

                if (curso.Costo != 0)
                {
                    cursoExistente.Costo = curso.Costo;
                }

                cursoExistente.Duracion = curso.Duracion ?? cursoExistente.Duracion;
                cursoExistente.Descripcion = curso.Descripcion ?? cursoExistente.Descripcion;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("Curso actualizado exitosamente");

                // Devolver una respuesta sin contenido (NoContent) para indicar que la actualización fue exitosa
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el curso");

                // Devolver una respuesta de error con un código de estado 500 (Internal Server Error) y un mensaje de error
                return StatusCode(500, $"Error al actualizar el curso: {ex.Message}");
            }
        }


        [HttpDelete("DeleteCurso/{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando curso con ID: {id}");

                var cursoExistente = await _context.Cursos.SingleOrDefaultAsync(c => c.Id == id);

                if (cursoExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si el curso no existe
                }

                _context.Cursos.Remove(cursoExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Curso eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Manejar excepciones de operación no válida (InvalidOperation)
                _logger.LogError(ex, "Error al eliminar el curso: Operación no válida");
                return StatusCode(500, $"Error al eliminar el curso: Operación no válida");
            }
            catch (Exception ex)
            {
                // Manejar excepciones genéricas
                _logger.LogError(ex, "Error al eliminar el curso");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar el curso: {ex.Message}");
            }
        }

      

    }
}
