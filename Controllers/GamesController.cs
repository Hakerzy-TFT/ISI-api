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

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly alvorContext _context;

        public GamesController(alvorContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {

            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame([FromBody]GameData req)
        {
            if (req.Title is null)
            {
                return BadRequest();
            }

            //create game page here
            GamePage gp= new()
            {
                BackgroundColor = req.BackgroundColor,
                Button1Url = req.Button1Url,
                Button2Url = req.Button2Url,
                Description = req.Description,
                FontColor = req.FontColor,
                Header = req.Header,
                Img1Src = req.Img1Src,
                Img2Src = req.Img2Src,
                Img3Src = req.Img3Src,
                BackgroundImage =req.BackgroundImage
            };

            _context.GamePages.Add(gp);
            await _context.SaveChangesAsync();
            //end

            string sqlcmd = $"EXEC gs_add_game " +
                $"@title='{req.Title}', " +
                $"@description='{req.Description}', " +
                $"@release_date='{req.ReleaseDate}', " +
                $"@studio_name='{req.GameStudioName}', " +
                $"@genre='{req.Genre}', " +
                $"@platform='{req.Platform}', " +
                $"@img_src='{req.ImgSrc}', " +
                $"@game_page_id={gp.Id}, " +
                $"@status_id ={req.StatusId};";

            Console.Write(sqlcmd);

            using (SqlConnection connection = new(_context.Database.GetConnectionString()))
            {
                var result = connection.Query<string>(sqlcmd);

                if (result.Any())
                {
                    //Console.WriteLine(result.First().ToString());
                    var studio = result.First().ToString();
                    return Ok(studio);
                }
                else
                {
                    return BadRequest();
                }
            }

            //_context.Games.Add(game);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
