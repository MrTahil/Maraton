using Maraton_api.Dto;
using Maraton_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maraton_api.Controllers
{
    [Route("FutokController")]
    [ApiController]
    public class Futok_controller : Controller
    {
        private readonly MaratonContext _context;

        public Futok_controller(MaratonContext context)
        {
            _context = context;
        }

        [HttpGet("ListOutCompetetitors")]
        public async Task<ActionResult<Futok>> FutokList()
        {
            return Ok(await _context.Futoks.ToListAsync());
        }

        [HttpDelete("DeleteFuto{Id}")]
        public async Task<ActionResult<Futok>> DeleteFuto(int id)
        {
            var os = await _context.Futoks.FirstOrDefaultAsync(x => x.Fid == id);
            if (os != null)
            {
                _context.Futoks.Remove(os);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Sikeresen törölve!" });
            }
            return NotFound();
        }

        [HttpPut("UpdateFutoDto{Id}")]
        public async Task<ActionResult<Futok>> UpdateFutoById(int Id, UpdateFutoDto updt)
        {
            var oke = await _context.Futoks.FirstOrDefaultAsync(x => x.Fid == Id);

            if (oke != null)
            {
                oke.Fnev = updt.Fnev;
                oke.Szulev = updt.Szulev;
                oke.Szulho = updt.Szulho;
                oke.Ffi = updt.Ffi;
                oke.Csapat = updt.Csapat;
                _context.Futoks.Update(oke);
                await _context.SaveChangesAsync();
                return Ok(oke);
            }
            return NotFound(new { message = "Nincs ilyen id-val rendelkező adat az adatbázisban." });
        }

        [HttpGet("ListFemaleRunners")]
        public async Task<ActionResult<IEnumerable<object>>> ListFemaleRunners()
        {
            var result = await _context.Futoks.Where(x => !x.Ffi).Select(x => new { x.Fnev, x.Szulev }).OrderBy(x => x.Fnev).ToListAsync();

            return Ok(result);
        }
    }
}
