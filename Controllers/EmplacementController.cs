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
    public class EmplacementController : ControllerBase
    {
        private readonly inventory_db_firstContext _context;

        public EmplacementController(inventory_db_firstContext context)
        {
            _context = context;
        }

        // GET: api/Emplacement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emplacement>>> GetEmplacement()
        {
            return await _context.Emplacement.Where(s=>s.IdEmplacementParent==null).ToListAsync();
        }

        // GET: api/Emplacement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emplacement>> GetEmplacement(ulong id)
        {
            var emplacement = await _context.Emplacement.FindAsync(id);

            if (emplacement == null)
            {
                return NotFound();
            }

            return emplacement;
        }
        [Route("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<Emplacement>>> GetSousEmplacments(ulong id)
        {
            var sousemplacments = await _context.Emplacement.Where(p => p.IdEmplacementParent == id).ToListAsync();
            return sousemplacments;
        }
        // PUT: api/Emplacement/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmplacement(ulong id, Emplacement emplacement)
        {
            if (id != emplacement.IdEmplacement)
            {
                return BadRequest();
            }

            _context.Entry(emplacement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmplacementExists(id))
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

        // POST: api/Emplacement
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Emplacement>> PostEmplacement(Emplacement emplacement)
        {
            _context.Emplacement.Add(emplacement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmplacement", new { id = emplacement.IdEmplacement }, emplacement);
        }

        // DELETE: api/Emplacement/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Emplacement>> DeleteEmplacement(ulong id)
        {
            var emplacement = await _context.Emplacement.FindAsync(id);
            if (emplacement == null)
            {
                return NotFound();
            }

            _context.Emplacement.Remove(emplacement);
            await _context.SaveChangesAsync();

            return emplacement;
        }

        private bool EmplacementExists(ulong id)
        {
            return _context.Emplacement.Any(e => e.IdEmplacement == id);
        }
    }
}
