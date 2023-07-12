using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<TransaccionController> _logger;

        public TransaccionController(PostgresContext context, ILogger<TransaccionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Transaccion
        [HttpGet("GetTransaccion")]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransaccions()
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

                var transaccionesDTO = transacciones.Select(p => new Transaccion
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
        public async Task<IActionResult> PutTransaccion(int id, [FromBody] Transaccion transaccion)
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
                if (transaccion.Tercero != 0)
                {
                    transaccionExistente.Tercero = transaccion.Tercero;
                }


                if (transaccion.Curso != null)
                {
                    transaccionExistente.Curso = transaccion.Curso;
                }

                transaccionExistente.Fechacompra = transaccion.Fechacompra ?? transaccion.Fechacompra;
                transaccionExistente.Metodopago = transaccion.Metodopago ?? transaccion.Metodopago;
                transaccionExistente.Datallesadicionales = transaccion.Datallesadicionales ?? transaccion.Datallesadicionales;
                transaccionExistente.Cupos = transaccion.Cupos ?? transaccion.Cupos;
                transaccionExistente.Codigo = transaccion.Codigo ?? transaccion.Codigo;

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaccion>> PostTransaccion(Transaccion transaccion)
        {
            if (_context.Transaccions == null)
            {
                return Problem("Entity set 'PostgresContext.Transaccions'  is null.");
            }
            _context.Transaccions.Add(transaccion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TransaccionExists(transaccion.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTransaccion", new { id = transaccion.Id }, transaccion);
        }

        // DELETE: api/Transaccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaccion(int id)
        {
            if (_context.Transaccions == null)
            {
                return NotFound();
            }
            var transaccion = await _context.Transaccions.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            _context.Transaccions.Remove(transaccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransaccionExists(int id)
        {
            return (_context.Transaccions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
