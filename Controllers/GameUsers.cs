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
using HakerzyLib.core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameUsers : ControllerBase
    {
        private readonly alvorContext _context;
        
        private readonly ILogger<Users> _logger;
        private readonly IConfiguration _configuration;

        public GameUsers(alvorContext context, IConfiguration configuration, ILogger<Users> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;

        }

        // GET: api/GameUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameUser>>> GetGameUsers()
        {
            return await _context.GameUsers.ToListAsync();
        }

        // GET: api/GameUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameUser>> GetGameUser(int id)
        {
            var gameUser = await _context.GameUsers.FindAsync(id);

            if (gameUser == null)
            {
                return NotFound();
            }

            return gameUser;
        }

        // PUT: api/GameUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameUser(int id, GameUser gameUser)
        {
            if (id != gameUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameUserExists(id))
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

        

        // POST: api/GameUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameUser>> PostGameUser(GameUserData gameUserData)
        {
            string sql = "select id from game where title='" + gameUserData.Title + "'";
            string sql2 = "select * from end_user where id=" + gameUserData.UserId + "";
            try
            {
                _logger.Log(LogLevel.Information, $"Called PostGameUserr() with game_title: ({gameUserData.Title}) userId: ({gameUserData.UserId})");
                using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    var result = connection.Query<int>(sql);
                    var result2 = connection.Query<string>(sql2);
                    if (!result.Any() || !result2.Any())
                    {
                        return BadRequest("Invalid data!");
                    }
                    var gameUser = new GameUser
                    {
                        EndUserId = gameUserData.UserId,
                        GameId = result.FirstOrDefault()
                    };
                    _context.GameUsers.Add(gameUser);
                    await _context.SaveChangesAsync();

                    return Ok(Message.ToJson("Game user is created"));
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtime - {e.Message}");
                throw;
            }
        }

        // DELETE: api/GameUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameUser(int id)
        {
            var gameUser = await _context.GameUsers.FindAsync(id);
            if (gameUser == null)
            {
                return NotFound();
            }

            _context.GameUsers.Remove(gameUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameUserExists(int id)
        {
            return _context.GameUsers.Any(e => e.Id == id);
        }
    }
}
