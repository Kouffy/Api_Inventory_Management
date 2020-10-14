using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventory_management_api.Models;

namespace inventory_management_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommercialController : ControllerBase
    {
        private readonly inventory_db_firstContext _context;

        public CommercialController(inventory_db_firstContext context)
        {
            _context = context;
        }

        // GET: api/Commercial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commercial>>> GetCommercial()
        {
            return await _context.Commercial.ToListAsync();
        }

        // GET: api/Commercial/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commercial>> GetCommercial(ulong id)
        {
            var commercial = await _context.Commercial.FindAsync(id);

            if (commercial == null)
            {
                return NotFound();
            }

            return commercial;
        }
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Commercial>> GetCommercialLogin(string login, string password)
        {
            var commercial = await _context.Commercial.FirstOrDefaultAsync(x => x.LoginCommercial == login && x.PasswordCommercial == password);

            if (commercial == null)
            {
                return NotFound();
            }

            return commercial;
        }
        // PUT: api/Commercial/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommercial(ulong id, Commercial commercial)
        {
            if (id != commercial.IdCommercial)
            {
                return BadRequest();
            }

            _context.Entry(commercial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommercialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Commercial
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Commercial>> PostCommercial(Commercial commercial)
        {
            _context.Commercial.Add(commercial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommercial", new { id = commercial.IdCommercial }, commercial);
        }

        // DELETE: api/Commercial/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Commercial>> DeleteCommercial(ulong id)
        {
            var commercial = await _context.Commercial.FindAsync(id);
            if (commercial == null)
            {
                return NotFound();
            }

            _context.Commercial.Remove(commercial);
            await _context.SaveChangesAsync();

            return commercial;
        }

        private bool CommercialExists(ulong id)
        {
            return _context.Commercial.Any(e => e.IdCommercial == id);
        }
    }
}
