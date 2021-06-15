using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gamespace_api.Models;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePages : ControllerBase
    {
        private readonly alvorContext _context;

        public GamePages(alvorContext context)
        {
            _context = context;
        }

        // GET: api/GamePages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamePage>>> GetGamePages()
        {
            return await _context.GamePages.ToListAsync();
        }

        // GET: api/GamePages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GamePage>> GetGamePage(int id)
        {
            var gamePage = await _context.GamePages.FindAsync(id);

            if (gamePage == null)
            {
                return NotFound();
            }

            return gamePage;
        }

        // PUT: api/GamePages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGamePage(int id, GamePage gamePage)
        {
            if (id != gamePage.Id)
            {
                return BadRequest();
            }

            _context.Entry(gamePage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamePageExists(id))
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

        // POST: api/GamePages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GamePage>> PostGamePage(GamePage gamePage)
        {
            _context.GamePages.Add(gamePage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGamePage", new { id = gamePage.Id }, gamePage);
        }

        // DELETE: api/GamePages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGamePage(int id)
        {
            var gamePage = await _context.GamePages.FindAsync(id);
            if (gamePage == null)
            {
                return NotFound();
            }

            _context.GamePages.Remove(gamePage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GamePageExists(int id)
        {
            return _context.GamePages.Any(e => e.Id == id);
        }
    }
}
