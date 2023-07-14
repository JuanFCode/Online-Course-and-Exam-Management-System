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

        // PUT: api/ExamenPresentado
        [HttpPut("Putpregunta/{id}")]
        public async Task<IActionResult> PutExamenPresentado(int id, [FromBody] Examenpresentado examenpresentado)
        {
            try
            {
                _logger.LogInformation($"Actualizando ExamenPresentado con ID : {id}");

                // Verificar si el ID en el cuerpo de la solicitud coincide con el ID proporcionado en la ruta
                if (id != examenpresentado.Id)
                {
                    return BadRequest("El ID de la transaccion en el cuerpo de la solicitud no coincide con el ID proporcionado en la ruta.");
                }

                // Buscar el curso existente en la base de datos utilizando el ID
                var ExamenPresentadoExistente = await _context.Examenpresentados.FindAsync(id);

                // Si no se encuentra la transaccion, devolver una respuesta de error NotFound
                if (ExamenPresentadoExistente == null)
                {
                    return NotFound($"No se encontró ExamenPresentado con ID: {id}");
                }

                ExamenPresentadoExistente.Tercero = examenpresentado.Tercero ?? ExamenPresentadoExistente.Tercero;
                ExamenPresentadoExistente.Examen = examenpresentado.Examen ?? ExamenPresentadoExistente.Examen;
                ExamenPresentadoExistente.Fechainicio = examenpresentado.Fechainicio ?? ExamenPresentadoExistente.Fechainicio;
                ExamenPresentadoExistente.Fechafinal = examenpresentado.Fechafinal ?? ExamenPresentadoExistente.Fechafinal;
                ExamenPresentadoExistente.Ultimapreguntarespondida = examenpresentado.Ultimapreguntarespondida ?? ExamenPresentadoExistente.Ultimapreguntarespondida;
                ExamenPresentadoExistente.Estadoexamen = examenpresentado.Estadoexamen ?? ExamenPresentadoExistente.Estadoexamen;



                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                _logger.LogInformation("ExamenPresentado actualizado exitosamente");

                // Devolver una respuesta sin contenido (NoContent) para indicar que la actualización fue exitosa
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar ExamenPresentado");

                // Devolver una respuesta de error con un código de estado 500 (Internal Server Error) y un mensaje de error
                return StatusCode(500, $"Error al actualizar ExamenPresentado: {ex.Message}");
            }
        }

        // DELETE: api/ExamenPresentado
        [HttpDelete("DeleteExamenPresentado/{id}")]
        public async Task<IActionResult> DeleteExamenPresentado(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando ExamenPresentado con ID: {id}");

                var ExamenPresentadoExistente = await _context.Examenpresentados.SingleOrDefaultAsync(c => c.Id == id);

                if (ExamenPresentadoExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si la transaccion no existe
                }

                _context.Examenpresentados.Remove(ExamenPresentadoExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("ExamenPresentado eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Manejar excepciones de operación no válida (InvalidOperation)
                _logger.LogError(ex, "Error al eliminar ExamenPresentado : Operación no válida");
                return StatusCode(500, $"Error al eliminar ExamenPresentado: Operación no válida");
            }
            catch (Exception ex)
            {
                // Manejar excepciones genéricas
                _logger.LogError(ex, "Error al eliminar la ExamenPresentado");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar ExamenPresentado : {ex.Message}");
            }
        }


    }
}
