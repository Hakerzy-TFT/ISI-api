using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gamespace_api.Models;
using gamespace_api.Models.DataTransfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Dapper;
using HakerzyLib.core;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Studios : ControllerBase
    {
        private readonly alvorContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Users> _logger;

        public Studios(alvorContext context, IConfiguration configuration, ILogger<Users> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        // GET: api/Studios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studio>>> GetStudios()
        {
            return await _context.Studios.ToListAsync();
        }

        // GET: api/Studios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Studio>> GetStudio(int id)
        {
            var studio = await _context.Studios.FindAsync(id);

            if (studio == null)
            {
                return NotFound();
            }

            return studio;
        }

        // PUT: api/Studios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudio(int id, Studio studio)
        {
            if (id != studio.Id)
            {
                return BadRequest();
            }

            _context.Entry(studio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudioExists(id))
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

        // POST: api/Studios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Studio>> PostStudio(StudioRegister studioObj)
        {
            try
            {
                string sql = "EXEC gs_get_studio_by_name @studio_name='" + studioObj.Name + "'";
                using(SqlConnection conn = new(_context.Database.GetConnectionString()))
                {
                    var res = conn.Query<string>(sql);

                    if (res.Any())
                    {
                        return BadRequest("{\"result\" : \"studio with that name already exists!\"}");
                    }
                    else
                    {
                        var studio = new Studio
                        {
                            Name = studioObj.Name,
                            Description = studioObj.Description,
                            Owner = studioObj.OwnerName
                        };
                        _context.Studios.Add(studio);
                        await _context.SaveChangesAsync();
                    }
                    return Ok(Message.ToJson("Studio created"));
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
            
        
            
        }

        // DELETE: api/Studios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudio(int id)
        {
            var studio = await _context.Studios.FindAsync(id);
            if (studio == null)
            {
                return NotFound();
            }

            _context.Studios.Remove(studio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudioExists(int id)
        {
            return _context.Studios.Any(e => e.Id == id);
        }
    }
}
