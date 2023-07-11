using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerceroController : ControllerBase
    {

        private readonly PostgresContext _context;
        private readonly ILogger<TerceroController> _logger;

        public TerceroController(PostgresContext context, ILogger<TerceroController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Obtiene la lista de los terceros
        [HttpGet("GetTercero")]
        public async Task<ActionResult<IEnumerable<Tercero>>> GetTercero()
        {
            try
            {
                _logger.LogInformation("Ejecutando consulta para obtener los países");

                // Ejecutar la consulta SQL para obtener los terceros directamente
                var terceros = await _context.Terceros.ToListAsync();

                _logger.LogInformation("Consulta ejecutada exitosamente");

                // Mapear los resultados a un DTO (Data Transfer Objects) de tercero para controlar los datos expuestos
                var terceroDTO = terceros.Select(p => new Tercero
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellidos = p.Apellidos,
                    Pais = p.Pais,
                    Correoelectronico = p.Correoelectronico,
                    Clave = p.Clave,
                    Tipo = p.Tipo

                }).ToList();

                // Devolver una respuesta HTTP 200 (OK) con la lista de terceros DTO
                return Ok(terceroDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los países");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al obtener los países: {ex.Message}");
            }
        }


        // Crea un nuevo Tercero.
        [HttpPost("PostTercero")]
        public async Task<IActionResult> CreateTercero([FromBody] Tercero tercero)
        {
            try
            {
                _logger.LogInformation("Creando nuevo tercero");

                // Crear una nueva instancia de la entidad  con los datos recibidos
                var nuevoTercero = new Tercero
                {
                    Id = tercero.Id,
                    Nombre = tercero.Nombre,
                    Apellidos = tercero.Apellidos,
                    Pais = tercero.Pais,
                    Correoelectronico = tercero.Correoelectronico,
                    Clave = tercero.Clave,
                    Tipo = tercero.Tipo
                };

                // Agregar la nueva entidad al contexto y guardar los cambios en la base de datos
                _context.Terceros.Add(nuevoTercero);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Tercero creado exitosamente");

                // Devolver una respuesta HTTP 201 (Created) con el nuevo país creado
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el tercero");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al crear el Tercero: {ex.Message}");
            }
        }



        [HttpPut("PutTercero/{id}")]
        public async Task<IActionResult> UpdateTercero(int id, [FromBody] Tercero tercero)
        {
            try
            {
                _logger.LogInformation($"Actualizando Tercero con ID: {id}");

                // Buscar el tercero existente en la base de datos
                var terceroExistente = await _context.Terceros.FindAsync(id);

                if (terceroExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si el tercero no existe
                }

                // Utilizar asignación condicional para actualizar los campos si no son nulos

                terceroExistente.Nombre ??= tercero.Nombre;
                terceroExistente.Apellidos ??= tercero.Apellidos;
                terceroExistente.Pais ??= tercero.Pais;
                terceroExistente.Correoelectronico ??= tercero.Correoelectronico;
                terceroExistente.Clave ??= tercero.Clave;
                terceroExistente.Tipo ??= tercero.Tipo;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("Tercero actualizado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la actualización fue exitosa
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el tercero");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al actualizar el país: {ex.Message}");
            }
        }

        [HttpDelete("DeleteTercero/{id}")]
        public async Task<IActionResult> DeleteTercero(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando tercero con ID: {id}");

                // Buscar el país existente en la base de datos
                var terceroExistente = await _context.Terceros.FindAsync(id);

                if (terceroExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si el tercero no existe
                }

                // Eliminar el tercero existente del contexto
                _context.Terceros.Remove(terceroExistente);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("País eliminado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el país");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al eliminar el país: {ex.Message}");
            }
        }

    }

}
