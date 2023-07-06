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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pai>>> GetPaisesFromStoredProcedure()
        {
            try
            {
                _logger.LogInformation("Ejecutando consulta del procedimiento almacenado");

                var paises = await _context.Pais.FromSqlRaw("SELECT * FROM mostrar_datos();").ToListAsync();

                _logger.LogInformation("Consulta ejecutada exitosamente");

                return Ok(paises);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los países desde el procedimiento almacenado");

                return StatusCode(500, $"Error al obtener los países desde el procedimiento almacenado: {ex.Message}");
            }
        }
    }
}
