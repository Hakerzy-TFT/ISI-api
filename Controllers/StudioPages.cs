using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gamespace_api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Dapper;
using HakerzyLib.core;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioPages : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<StudioPages> _logger;
        private readonly alvorContext _context;

        public StudioPages(alvorContext context, IConfiguration configuration, ILogger<StudioPages> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }



        // GET: api/StudioPages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudioPage>>> GetStudioPages()
        {
            return await _context.StudioPages.ToListAsync();
        }

        // GET: api/StudioPages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudioPage>> GetStudioPage(int id)
        {
            var studioPage = await _context.StudioPages.FindAsync(id);

            if (studioPage == null)
            {
                return NotFound();
            }

            return studioPage;
        }
        [HttpGet("studio-and-page")]
        public ActionResult<EndUser> GetStudioAndPage()
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Called GetStudioAndPage");

                

                string sql = "EXEC gs_get_studio_page_and_studio";


                using (SqlConnection connection = new(_context.Database.GetConnectionString()))
                {
                    var result = connection.Query<string>(sql);

                    if (result.Any())
                    {
                        return Ok(result.First().ToString());
                    }
                    else
                    {
                        return BadRequest(Message.ToJson("Page not found"));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }
        // PUT: api/StudioPages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudioPage(int id, StudioPage studioPage)
        {
            if (id != studioPage.Id)
            {
                return BadRequest();
            }

            _context.Entry(studioPage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudioPageExists(id))
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

        // POST: api/StudioPages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudioPage>> PostStudioPage(StudioPage studioPage)
        {
            _context.StudioPages.Add(studioPage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudioPage", new { id = studioPage.Id }, studioPage);
        }

        // DELETE: api/StudioPages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudioPage(int id)
        {
            var studioPage = await _context.StudioPages.FindAsync(id);
            if (studioPage == null)
            {
                return NotFound();
            }

            _context.StudioPages.Remove(studioPage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudioPageExists(int id)
        {
            return _context.StudioPages.Any(e => e.Id == id);
        }
    }
}
