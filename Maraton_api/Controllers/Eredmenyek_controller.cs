using Maraton_api.Dto;
using Maraton_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maraton_api.Controllers
{
    [Route("EredmenyekController")]
    [ApiController]
    public class Eredmenyek_controller : Controller
    {
        private readonly MaratonContext _context;

        public Eredmenyek_controller(MaratonContext context)
        {
            _context = context;
        }


        [HttpGet("GetResultsForRunner/{Id}")]
        public async Task<ActionResult<IEnumerable<Eredmenyek>>> GetResultsForRunner(int Id)
        {
            var results = await _context.Eredmenyeks.Where(x => x.Futo == Id).ToListAsync();
    


            if (!results.Any())
            {
                return NotFound(new { message = "Nincs eredmény az adott futóhoz." });
            }

            return Ok(results);
        }

        [HttpPut("UpdateRunnerResults/{resultId}")]
        public async Task<ActionResult<Eredmenyek>> UpdateRunnerResults(int resultId, UpdateEredmenyekDto updatedResult)
        {
            // Az Eid mezőt kell használni az eredmény azonosításához
            var result = await _context.Eredmenyeks.FirstOrDefaultAsync(x => x.Futo == resultId);

            if (result == null)
            {
                return NotFound(new { message = "Nincs ilyen id-val rendelkező eredmény az adatbázisban." });
            }

            // Frissítsük az eredmény mezőit a DTO alapján
            result.Ido = updatedResult.Ido;
            result.Kor = updatedResult.Kor; // Feltételezve, hogy a Kor is frissíthető
            result.Futo = updatedResult.Futo; // Ha a futó ID-t is frissíteni szeretnéd
            _context.Eredmenyeks.Update(result);

            await _context.SaveChangesAsync();

            return Ok(result);
        }

    }
}
