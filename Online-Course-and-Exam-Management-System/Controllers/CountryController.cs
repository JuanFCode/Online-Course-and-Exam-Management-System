using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly PostgresContext _context;

        public CountriesController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet("paises")]
        public async Task<ActionResult<IEnumerable<Pai>>> GetPaisesFromStoredProcedure()
        {
            try
            {
                var paises = await _context.Pais.FromSqlRaw("CALL public.getpaises()").ToListAsync();
                return Ok(paises);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los países desde el procedimiento almacenado: {ex.Message}");
            }
        }

    
    }
}
