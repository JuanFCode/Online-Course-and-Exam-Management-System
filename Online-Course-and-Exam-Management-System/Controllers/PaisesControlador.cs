using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Online_Course_and_Exam_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesControlador : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<PaisesControlador> _logger;

        public PaisesControlador(PostgresContext context, ILogger<PaisesControlador> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Obtiene la lista de países desde un procedimiento almacenado.
        [HttpGet("GetPais")]
        public async Task<ActionResult<IEnumerable<Paises>>> GetPaisesFromStoredProcedure()
        {
            try
            {
                _logger.LogInformation("Ejecutando consulta para obtener los países");

                // Ejecutar la consulta SQL para obtener los países directamente
                var paises = await _context.Pais.ToListAsync();

                _logger.LogInformation("Consulta ejecutada exitosamente");

                // Mapear los resultados a un DTO (Data Transfer Objects) de país para controlar los datos expuestos
                var paisDTO = paises.Select(p => new Paises
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                }).ToList();

                // Devolver una respuesta HTTP 200 (OK) con la lista de países DTO
                return Ok(paisDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los países");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al obtener los países: {ex.Message}");
            }
        }

        // Crea un nuevo país.
        [HttpPost("PostPais")]
        public async Task<IActionResult> CreatePais([FromBody] Paises pai)
        {
            try
            {
                _logger.LogInformation("Creando nuevo país");

                // Crear una nueva instancia de la entidad Pais con los datos recibidos
                var nuevoPais = new Paises
                {
                    Id = pai.Id,
                    Nombre = pai.Nombre
                };

                // Agregar la nueva entidad al contexto y guardar los cambios en la base de datos
                _context.Pais.Add(nuevoPais);
                await _context.SaveChangesAsync();

                _logger.LogInformation("País creado exitosamente");

                // Devolver una respuesta HTTP 201 (Created) con el nuevo país creado
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el país");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al crear el país: {ex.Message}");
            }
        }


        [HttpPut("PutPais/{id}")]
        public async Task<IActionResult> UpdatePais(int id, [FromBody] Paises pai)
        {
            try
            {
                _logger.LogInformation($"Actualizando país con ID: {id}");

                // Buscar el país existente en la base de datos
                var paisExistente = await _context.Pais.FindAsync(id);

                if (paisExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si el país no existe
                }

                // Actualizar los datos del país existente con los nuevos datos recibidos
                paisExistente.Nombre = pai.Nombre;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("País actualizado exitosamente");

                // Devolver una respuesta HTTP 204 (No Content) para indicar que la actualización fue exitosa
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el país");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al actualizar el país: {ex.Message}");
            }
        }


        [HttpDelete("DeletePais/{id}")]
        public async Task<IActionResult> DeletePais(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando país con ID: {id}");

                // Buscar el país existente en la base de datos
                var paisExistente = await _context.Pais.FindAsync(id);

                if (paisExistente == null)
                {
                    return NotFound(); // Devolver una respuesta HTTP 404 (Not Found) si el país no existe
                }

                // Eliminar el país existente del contexto
                _context.Pais.Remove(paisExistente);

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
