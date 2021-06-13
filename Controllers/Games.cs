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
    public class Games : ControllerBase
    {
        private readonly alvorContext _context;

        public Games(alvorContext context)
        {
            _context = context;
        }

        // GET: api/Games/Rankings
        [HttpPost]
        [Route("Rankings")]
        public ActionResult<IEnumerable<Game>> GetRankings([FromBody] RankingRequest req)
        {
            string sqlcmd = $"EXEC gs_get_rankings " +
                $"@platform='{req.Platform}', " +
                $"@criterium='{req.Criterium}', " +
                $"@genre = '{req.Genre}';";

            Console.WriteLine(sqlcmd);

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
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            var gamePage = _context.GamePages.FirstOrDefault(s => s.Id == game.GamePageId);
            var studio = await _context.Studios.FindAsync(game.StudioId);

            GameDto dto = new()
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                ReleaseDate = game.ReleaseDate,
                PostedDate = game.PostedDate,
                TotalRating = game.TotalRating,
                ImgSrc = game.ImgSrc,
                Img1Src = gamePage.Img1Src,
                Img2Src = gamePage.Img2Src,
                Img3Src = gamePage.Img3Src,
                Header = gamePage.Header,
                FontColor = gamePage.FontColor,
                ButtonColor = gamePage.ButtonColor,
                StudioName = studio.Name
            };

            if (game == null)
            {
                return NotFound();
            }

            return dto;
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
