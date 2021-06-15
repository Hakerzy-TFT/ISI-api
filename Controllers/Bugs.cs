using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gamespace_api.Models;
using Microsoft.Extensions.Logging;
using gamespace_api.Models.DataTransfer;
using Microsoft.Data.SqlClient;
using Dapper;
using HakerzyLib.core;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Bugs : ControllerBase
    {
        private readonly alvorContext _context;
        private readonly ILogger<Users> _logger;

        

        public Bugs(alvorContext context, ILogger<Users> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Bugs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bug>>> GetBugs()
        {
            return await _context.Bugs.ToListAsync();
        }

        // GET: api/Bugs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bug>> GetBug(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);

            if (bug == null)
            {
                return NotFound();
            }

            return bug;
        }

        // PUT: api/Bugs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBug(int id, Bug bug)
        {
            if (id != bug.Id)
            {
                return BadRequest();
            }

            _context.Entry(bug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(id))
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

        // POST: api/Bugs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bug>> PostBug(BugRequest bugRequest)
        {
            string sql = "select * from game where id = " + bugRequest.GameId + "";

            try
            {
                _logger.Log(LogLevel.Information, $"Called PostReview()");
                using (SqlConnection connection = new(_context.Database.GetConnectionString()))
                {
                    var result = connection.Query<int>(sql);
                    if (!result.Any())
                    {
                        return BadRequest("This game does not exist");
                    }
                    
                    sql = "select id from status where name = 'New'";
                    var result2 = connection.Query<int>(sql);

                    var bug = new Bug
                    {
                        Title = bugRequest.Title,
                        Description= bugRequest.Description,
                        DateOfCreation = DateTime.Now,
                        EndUserId = bugRequest.EndUserId,
                        StatusId = result2.First()
                    };

                    _context.Bugs.Add(bug);
                    await _context.SaveChangesAsync();

                    sql = "INSERT INTO game_bug VALUES(" + bugRequest.GameId + "," + bug.Id + ")";
                    var result3 = connection.Query<int>(sql);
                }

                return Ok(Message.ToJson("Bug is created"));
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }

        // DELETE: api/Bugs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBug(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }

            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BugExists(int id)
        {
            return _context.Bugs.Any(e => e.Id == id);
        }
    }
}
