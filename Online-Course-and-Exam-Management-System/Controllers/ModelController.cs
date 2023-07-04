using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Course_and_Exam_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("paises")]
        public async Task<ActionResult<List<Pai>>> GetPaises()
        {
            try
            {
                var paises = await _context.Pais.ToListAsync();
                return Ok(paises);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al obtener los países: " + ex.Message);
            }
        }

        [HttpGet("paises/procedure")]
        public ActionResult<List<Pai>> GetPaisesFromStoredProcedure()
        {
            try
            {
                var paises = _context.Pais.FromSqlRaw("EXECUTE public.getpaises()").ToList();

                return Ok(paises);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al obtener los países: " + ex.Message);
            }
        }
    }
}
