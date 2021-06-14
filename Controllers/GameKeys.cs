using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gamespace_api.Models;
using gamespace_api.Models.DataTransfer;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameKeys : ControllerBase
    {
        private readonly alvorContext _context;
        private readonly IConfiguration _configuration;

        public GameKeys(alvorContext context, IConfiguration config)
        {
            _configuration = config;
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

        [HttpPost]
        [Route("AssingKey")]
        public ActionResult<GameKey> PostAssignKey([FromBody] GameKeyDto gameKey)
        {
            if (HakerzyLib.Core.Utils.IsAnyNullOrEmpty(gameKey))
            {
                return BadRequest("Wrong input data!");
            }

            string sqlcmd = $"EXEC gs_assign_key @gameTitle = '{gameKey.GameName}', @username = '{gameKey.Username}';";

            using (SqlConnection conn = new(_context.Database.GetConnectionString()))
            {
                var result = conn.Query<string>(sqlcmd);
                if (result.Any())
                {
                    var key = result.First().ToString();
                    return Ok(key);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // POST: api/GameKeys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<GameKey> PostGameKey([FromBody] GameKeyDto gameKey)
        {
            if (HakerzyLib.Core.Utils.IsAnyNullOrEmpty(gameKey))
            {
                return BadRequest("Wrong input data!");
            }

            string sqlcmd = $"EXEC  gs_add_key @gameTitle = '{gameKey.GameName}', @key = '{gameKey.Value}';";

            using (SqlConnection conn = new(_context.Database.GetConnectionString()))
            {
                var result = conn.Query<string>(sqlcmd);
                if (result.Any())
                {
                    var key = result.First().ToString();
                    return Ok(key);
                }
                else
                {
                    return BadRequest();
                }
            }
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
