using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gamespace_api.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.IdentityModel.Protocols;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly alvorContext _context;

        public Users(alvorContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EndUser>>> GetEndUsers()
        {
            return await _context.EndUsers.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EndUser>> GetEndUser(int id)
        {
            var endUser = await _context.EndUsers.FindAsync(id);

            if (endUser == null)
            {
                return NotFound();
            }

            return endUser;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndUser(int id, EndUser endUser)
        {
            if (id != endUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(endUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndUserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EndUser>> PostEndUser(EndUser endUser)
        {
            string sql = "EXEC   gs_get_user_by_email @email= '" + endUser.Email + "'";
            

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var result = connection.Query<string>(sql);
                Console.WriteLine("hehehe");
                if (result.Any())
                {
                    Console.WriteLine(result.First());
                    return BadRequest("{\"result\" : \"user already exists!\"}");
                }
                else
                {
                    _context.EndUsers.Add(endUser);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetEndUser", new { id = endUser.Id }, endUser);
                }
                
                
            }

            
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndUser(int id)
        {
            var endUser = await _context.EndUsers.FindAsync(id);
            if (endUser == null)
            {
                return NotFound();
            }

            _context.EndUsers.Remove(endUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EndUserExists(int id)
        {
            return _context.EndUsers.Any(e => e.Id == id);
        }
    }
}
