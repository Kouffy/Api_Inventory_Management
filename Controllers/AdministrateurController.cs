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
    public class AdministrateurController : ControllerBase
    {
        private readonly inventory_db_firstContext _context;

        public AdministrateurController(inventory_db_firstContext context)
        {
            _context = context;
        }

        // GET: api/Administrateur
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrateur>>> GetAdministrateur()
        {
            return await _context.Administrateur.ToListAsync();
        }

        // GET: api/Administrateur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrateur>> GetAdministrateur(ulong id)
        {
            var administrateur = await _context.Administrateur.FindAsync(id);

            if (administrateur == null)
            {
                return NotFound();
            }

            return administrateur;
        }
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Administrateur>> GetAdministrateurLogin(string login, string password)
        {
            var administrateur = await _context.Administrateur.FirstOrDefaultAsync(x => x.LoginAdministrateur == login && x.PasswordAdministrateur == password);

            if (administrateur == null)
            {
                return NotFound();
            }

            return administrateur;
        }
        // PUT: api/Administrateur/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrateur(ulong id, Administrateur administrateur)
        {
            if (id != administrateur.IdAdministrateur)
            {
                return BadRequest();
            }

            _context.Entry(administrateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministrateurExists(id))
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

        // POST: api/Administrateur
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Administrateur>> PostAdministrateur(Administrateur administrateur)
        {
            _context.Administrateur.Add(administrateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministrateur", new { id = administrateur.IdAdministrateur }, administrateur);
        }

        // DELETE: api/Administrateur/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Administrateur>> DeleteAdministrateur(ulong id)
        {
            var administrateur = await _context.Administrateur.FindAsync(id);
            if (administrateur == null)
            {
                return NotFound();
            }

            _context.Administrateur.Remove(administrateur);
            await _context.SaveChangesAsync();

            return administrateur;
        }

        private bool AdministrateurExists(ulong id)
        {
            return _context.Administrateur.Any(e => e.IdAdministrateur == id);
        }
    }
}
