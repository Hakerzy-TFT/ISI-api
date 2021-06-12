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
    public class GameKeys : ControllerBase
    {
        private readonly alvorContext _context;

        public GameKeys(alvorContext context)
        {
            _context = context;
        }

        // GET: api/GameKeys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameKey>>> GetGameKeys()
        {
            return await _context.GameKeys.ToListAsync();
        }

        // GET: api/GameKeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameKey>> GetGameKey(int id)
        {
            var gameKey = await _context.GameKeys.FindAsync(id);

            if (gameKey == null)
            {
                return NotFound();
            }

            return gameKey;
        }

        // PUT: api/GameKeys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameKey(int id, GameKey gameKey)
        {
            if (id != gameKey.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameKey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameKeyExists(id))
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

        // POST: api/GameKeys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameKey>> PostGameKey(GameKey gameKey)
        {
            _context.GameKeys.Add(gameKey);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameKey", new { id = gameKey.Id }, gameKey);
        }

        // DELETE: api/GameKeys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameKey(int id)
        {
            var gameKey = await _context.GameKeys.FindAsync(id);
            if (gameKey == null)
            {
                return NotFound();
            }

            _context.GameKeys.Remove(gameKey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameKeyExists(int id)
        {
            return _context.GameKeys.Any(e => e.Id == id);
        }
    }
}
