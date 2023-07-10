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
    public class CountriesController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(PostgresContext context, ILogger<CountriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Obtiene la lista de países desde un procedimiento almacenado.
        [HttpGet("GetPais")]
        public async Task<ActionResult<IEnumerable<Pai>>> GetPaisesFromStoredProcedure1()
        {
            try
            {
                _logger.LogInformation("Ejecutando consulta del procedimiento almacenado");

                // Ejecutar la consulta SQL para obtener los países desde el procedimiento almacenado
                var paises = await _context.Pais.FromSqlRaw("SELECT id,nombre FROM mostrar_datos();").ToListAsync();

                _logger.LogInformation("Consulta ejecutada exitosamente");

                // Mapear los resultados a un DTO ( es Data Transfer Objects ) de país para controlar los datos expuestos
                var paisDTO = paises.Select(p => new Pai
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                }).ToList();
                // Devolver una respuesta HTTP 200 (OK) con la lista de países DTO
                return Ok(paisDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los países desde el procedimiento almacenado");

                // Devolver una respuesta HTTP 500 (Internal Server Error) con un mensaje de error
                return StatusCode(500, $"Error al obtener los países desde el procedimiento almacenado: {ex.Message}");
            }
        }

        // Crea un nuevo país.
        [HttpPost("CreatePais")]
        public async Task<IActionResult> CreatePais([FromBody] Pai pai)
        {
            try
            {
                _logger.LogInformation("Creando nuevo país");

                // Llamar a la función de inserción de datos en PostgreSQL
                await _context.Database.ExecuteSqlInterpolatedAsync($@"SELECT insertar_datos({pai.Id}, {pai.Nombre})");

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


    }
}
