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
    public class ArticleController : ControllerBase
    {
        private readonly inventory_db_firstContext _context;

        public ArticleController(inventory_db_firstContext context)
        {
            _context = context;
        }

        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticle()
        {
            return await _context.Article.ToListAsync();
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(ulong id)
        {
            var article = await _context.Article.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }
        [Route("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesEmplacment(ulong id)
        {
            var articles = await _context.Article.Where(p => p.IdEmplacement == id).ToListAsync();
            return articles;
        }
        [Route("[action]/{id}")]
        public async Task<ActionResult<ulong>> GetArticleIdEmplacment(ulong id)
        {
            var article = await _context.Article.FindAsync(id);
            return article.IdEmplacement;
        }
        // PUT: api/Article/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(ulong id, Article article)
        {
            if (id != article.IdArticle)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Article
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            var idempr = await _context.Emplacement.FindAsync(article.IdEmplacement);
            if(idempr.IdEmplacementParent == null){return BadRequest();}
            _context.Article.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.IdArticle }, article);
        }

        // DELETE: api/Article/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Article>> DeleteArticle(ulong id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            return article;
        }

        private bool ArticleExists(ulong id)
        {
            return _context.Article.Any(e => e.IdArticle == id);
        }
    }
}
