using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<ActionResult<List<Pai>>> Getmodels()
        {
            // Muestra en la entidad pais donde el id se 1 
           // return Ok(await _context.Pais.Where(auth => auth.Id == 1).ToListAsync());

            // Muestra todos los datos de la entidad Pai (que es Pais)
           return Ok(await _context.Pais.ToListAsync());

        }
    }
}
