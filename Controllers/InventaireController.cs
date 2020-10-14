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
    public class InventaireController : ControllerBase
    {
        private readonly inventory_db_firstContext _context;

        public InventaireController(inventory_db_firstContext context)
        {
            _context = context;
        }

        // GET: api/Inventaire
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventaire>>> GetInventaire()
        {
            return await _context.Inventaire.ToListAsync();
        }

        // GET: api/Inventaire/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventaire>> GetInventaire(ulong id)
        {
            var inventaire = await _context.Inventaire.FindAsync(id);

            if (inventaire == null)
            {
                return NotFound();
            }

            return inventaire;
        }

        // PUT: api/Inventaire/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventaire(ulong id, Inventaire inventaire)
        {
            if (id != inventaire.IdInventaire)
            {
                return BadRequest();
            }

            _context.Entry(inventaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventaireExists(id))
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

        // POST: api/Inventaire
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Inventaire>> PostInventaire(Inventaire inventaire)
        {
            _context.Inventaire.Add(inventaire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventaire", new { id = inventaire.IdInventaire }, inventaire);
        }

        // DELETE: api/Inventaire/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Inventaire>> DeleteInventaire(ulong id)
        {
            var inventaire = await _context.Inventaire.FindAsync(id);
            if (inventaire == null)
            {
                return NotFound();
            }

            _context.Inventaire.Remove(inventaire);
            await _context.SaveChangesAsync();

            return inventaire;
        }

        private bool InventaireExists(ulong id)
        {
            return _context.Inventaire.Any(e => e.IdInventaire == id);
        }
    }
}
