using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<TransaccionesController> _logger;

        public TransaccionesController(PostgresContext context, ILogger<TransaccionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Transaccion
        [HttpGet("GetTransaccion")]
        public async Task<ActionResult<IEnumerable<Transacciones>>> GetTransaccions()
        {
            try
            {

                _logger.LogInformation("Obteniendo transacciones");

                var transacciones = await _context.Transaccions.ToListAsync();

                if (transacciones == null || !transacciones.Any())
                {
                    _logger.LogInformation("No se encontraron cursos");

                    return NotFound(" No se encontraron cursos");
                }

                var transaccionesDTO = transacciones.Select(p => new Transacciones
                {

                    Id = p.Id,
                    Tercero = p.Tercero,
                    Curso = p.Curso,
                    Fechacompra = p.Fechacompra,
                    Metodopago = p.Metodopago,
                    Datallesadicionales = p.Datallesadicionales,
                    Cupos = p.Cupos,
                    Codigo = p.Codigo

                }).ToList();
                _logger.LogInformation("Transacciones obtenidos exitosamente");
                return Ok(transaccionesDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las transacciones");

                return StatusCode(500, $"Error al obtener las transacciones: {ex.Message}");
            }


        }



        // PUT: api/Transaccion
        [HttpPut("putTransaccion/{id}")]
        public async Task<IActionResult> PutTransaccion(int id, [FromBody] Transacciones transaccion)
        {
            try
            {
                _logger.LogInformation($"Actualizando transaccion con ID : {id}");

                // Verificar si el ID en el cuerpo de la solicitud coincide con el ID proporcionado en la ruta
                if (id != transaccion.Id)
                {
                    return BadRequest("El ID de la transaccion en el cuerpo de la solicitud no coincide con el ID proporcionado en la ruta.");
                }

                // Buscar el curso existente en la base de datos utilizando el ID
                var transaccionExistente = await _context.Transaccions.FindAsync(id);

                // Si no se encuentra la transaccion, devolver una respuesta de error NotFound
                if (transaccionExistente == null)
                {
                    return NotFound($"No se encontró la transaccion con ID: {id}");
                }

                // Actualizar los campos individuales del curso existente solo si han sido modificados
                if (transaccion.Tercero != null  )
                {
                    transaccionExistente.Tercero = transaccion.Tercero;
                }


                if (transaccion.Curso != null)
                {
                    transaccionExistente.Curso = transaccion.Curso;
                }

                transaccionExistente.Fechacompra = transaccion.Fechacompra ?? transaccionExistente.Fechacompra;
                transaccionExistente.Metodopago = transaccion.Metodopago ?? transaccionExistente.Metodopago;
                transaccionExistente.Datallesadicionales = transaccion.Datallesadicionales ?? transaccionExistente.Datallesadicionales;
                transaccionExistente.Cupos = transaccion.Cupos ?? transaccionExistente.Cupos;
                transaccionExistente.Codigo = transaccion.Codigo ?? transaccionExistente.Codigo;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                _logger.LogInformation("Transaccion actualizado exitosamente");

                // Devolver una respuesta sin contenido (NoContent) para indicar que la actualización fue exitosa
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la transaccion");

                // Devolver una respuesta de error con un código de estado 500 (Internal Server Error) y un mensaje de error
                return StatusCode(500, $"Error al actualizar la transaccion: {ex.Message}");
            }
        }

        // POST: api/Transaccion
        [HttpPost("PostTransaccion")]
        public async Task<IActionResult> postTransaccion([FromBody] Transacciones transaccion)
        {
            try
            {
                _logger.LogInformation("Creando nuevo transaccion");

                // Agregar el objeto transaccioon a la colección de Transacciones en el contexto
                _context.Transaccions.Add(transaccion);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("Transaccion creado exitosamente");

                // Devolver un código de estado 201 (Created) para indicar que el recurso se creó correctamente
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear transaccion");

                // Devolver un código de estado 500 (Internal Server Error) y un mensaje de error en caso de excepción
                return StatusCode(500, $"Error al crear la transaccions: {ex.Message}");
            }
        }

        // DELETE: api/Transaccion/5
        [HttpDelete("DeleteTransaccion/{id}")]
        public async Task<IActionResult> DeleteTransaccion(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando transaccion con ID: {id}");

                var transaccionExistente = await _context.Transaccions.SingleOrDefaultAsync(c => c.Id == id);

                if (transaccionExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si la transaccion no existe
                }

                _context.Transaccions.Remove(transaccionExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Transaccion eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Manejar excepciones de operación no válida (InvalidOperation)
                _logger.LogError(ex, "Error al eliminar la transaccion: Operación no válida");
                return StatusCode(500, $"Error al eliminar la transaccion: Operación no válida");
            }
            catch (Exception ex)
            {
                // Manejar excepciones genéricas
                _logger.LogError(ex, "Error al eliminar la transaccion");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar la transaccion : {ex.Message}");
            }
        }






    }
}
