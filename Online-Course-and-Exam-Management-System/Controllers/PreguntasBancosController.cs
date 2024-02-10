using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasBancosController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<PreguntasBancosController> _logger;
        
        public PreguntasBancosController(PostgresContext context, ILogger<PreguntasBancosController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // Get: api/PreguntaBanco
        [HttpGet("GetPreguntaBanco")]
        public async Task<ActionResult<IEnumerable<Preguntasbancos>>> GetPreguntaBanco()
        {
            try
            {

                _logger.LogInformation("Obteniendo preguntaBanco");

                var preguntaBanco = await _context.Preguntabancos.ToListAsync();

                if (preguntaBanco == null || !preguntaBanco.Any())
                {
                    _logger.LogInformation("No se encontraron preguntaBanco");

                    return NotFound(" No se encontraron preguntaBanco");
                }

                var preguntaBancoDTO = preguntaBanco.Select(p => new Preguntasbancos
                {

                    Id = p.Id,
                    Curso = p.Curso,
                    Tema = p.Tema,
                    Enunciado = p.Enunciado,
                    Explicacion = p.Explicacion

                }).ToList();
                _logger.LogInformation("preguntaBanco obtenidos exitosamente");
                return Ok(preguntaBancoDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener preguntaBanco");

                return StatusCode(500, $"Error al obtener preguntaBanco: {ex.Message}");
            }

        }

        // POST: api/preguntaBanco
        [HttpPost("PostpreguntaBanco")]
        public async Task<IActionResult> PostpreguntaBanco([FromBody] Preguntasbancos preguntabanco)
        {
            try
            {
                _logger.LogInformation("Creando nuevo preguntaBanco");

                // Agregar el objeto preguntaBanco a la colección de Transacciones en el contexto
                _context.Preguntabancos.Add(preguntabanco);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("preeguntaBanco creado exitosamente");

                // Devolver un código de estado 201 (Created) para indicar que el recurso se creó correctamente
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear preguntaBanco");

                // Devolver un código de estado 500 (Internal Server Error) y un mensaje de error en caso de excepción
                return StatusCode(500, $"Error al crear el preguntaBanco: {ex.Message}");
            }
        }

        // PUT: api/preguntaBanco
        [HttpPut("PutpreguntaBanco/{id}")]
        public async Task<IActionResult> PutpreguntaBanco(int id, [FromBody] Preguntasbancos preguntabanco)
        {
            try
            {
                _logger.LogInformation($"Actualizando preguntaBanco con ID : {id}");

                // Verificar si el ID en el cuerpo de la solicitud coincide con el ID proporcionado en la ruta
                if (id != preguntabanco.Id)
                {
                    return BadRequest("El ID de la transaccion en el cuerpo de la solicitud no coincide con el ID proporcionado en la ruta.");
                }

                // Buscar el curso existente en la base de datos utilizando el ID
                var preguntaBancoExistente = await _context.Preguntabancos.FindAsync(id);

                // Si no se encuentra la transaccion, devolver una respuesta de error NotFound
                if (preguntaBancoExistente == null)
                {
                    return NotFound($"No se encontró preguntaBanco con ID: {id}");
                }

                preguntaBancoExistente.Curso = preguntabanco.Curso ?? preguntaBancoExistente.Curso;
                preguntaBancoExistente.Tema = preguntabanco.Tema ?? preguntaBancoExistente.Tema;
                preguntaBancoExistente.Enunciado = preguntabanco.Enunciado ?? preguntaBancoExistente.Enunciado;
                preguntaBancoExistente.Explicacion = preguntabanco.Explicacion ?? preguntaBancoExistente.Explicacion;


                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                _logger.LogInformation("preguntaBanco actualizado exitosamente");

                // Devolver una respuesta sin contenido (NoContent) para indicar que la actualización fue exitosa
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar preguntaBanco");

                // Devolver una respuesta de error con un código de estado 500 (Internal Server Error) y un mensaje de error
                return StatusCode(500, $"Error al actualizar preguntaBanco: {ex.Message}");
            }
        }

        // DELETE: api/preguntaBanco
        [HttpDelete("DeleteprpeguntaBanco/{id}")]
        public async Task<IActionResult> DeletepreguntaBanco(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando PregeuntaBanco con ID: {id}");

                var preguntaBancoExistente = await _context.Preguntabancos.SingleOrDefaultAsync(c => c.Id == id);

                if (preguntaBancoExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si la transaccion no existe
                }

                _context.Preguntabancos.Remove(preguntaBancoExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("preguntaBanco eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Manejar excepciones de operación no válida (InvalidOperation)
                _logger.LogError(ex, "Error al eliminar preguntaBanco : Operación no válida");
                return StatusCode(500, $"Error al eliminar PreguntaBanco: Operación no válida");
            }
            catch (Exception ex)
            {
                // Manejar excepciones genéricas
                _logger.LogError(ex, "Error al eliminar la PreguntaBanco");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar PreguntaBanco : {ex.Message}");
            }
        }

    }
}
