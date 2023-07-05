using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;

namespace Online_Course_and_Exam_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly PostgresContext _context;

        public ModelController(PostgresContext context)
        {
            _context = context;
        }

        // metodo get() para mostrar todos los datos de la tabla pais
        [HttpGet("procedimiento")]
        public async Task<ActionResult<IEnumerable<Pai>>> GetPaisesFromStoredProcedure()
        {
            try
            {
                var paises = await _context.Pais.FromSqlRaw("SELECT * FROM getpaises()").ToListAsync();
                return Ok(paises);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al obtener los países desde el procedimiento almacenado: " + ex.Message);
            }
        }

    }
}


