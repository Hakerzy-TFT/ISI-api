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
using gamespace_api.Models.DataTransfer;
using HakerzyLib.core;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Reviews : ControllerBase
    {
        private readonly alvorContext _context;
        private readonly ILogger<Reviews> _logger;

        public Reviews(alvorContext context, ILogger<Reviews> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }
        // GET: api/Reviews/5
        [HttpGet("bygameid/{id}")]
        public ActionResult<Review> GetReviewByGameId(int id)
        {
            string sql = "EXEC gs_get_review_by_gameId @game_id= '" + id + "'";

            try
            {
                _logger.Log(LogLevel.Information, $"Called GetReviewByGameId()");
                using (SqlConnection connection = new(_context.Database.GetConnectionString()))
                {
                    var result = connection.Query<string>(sql);
                    if (result.Any())
                    {
                        return Ok(result.First().ToString());
                    }
                    else
                    {
                        return BadRequest(Message.ToJson("Game not found!"));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(ReviewRequest reviewRequest)
        {
            string sql = "select * from game where id = " + reviewRequest.GameId + "";

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
                    if(reviewRequest.Rating<0 || reviewRequest.Rating > 10)
                    {
                        return BadRequest("Bad rating");
                    }
                    sql = "select id from status where name = 'New'";
                    var result2 = connection.Query<int>(sql);
                     
                    var review = new Review
                    {
                        Rating = reviewRequest.Rating,
                        ReviewContent = reviewRequest.ReviewContent,
                        EndUserId = reviewRequest.EndUserId,
                        StatusId=result.First()
                    };
                    
                    _context.Reviews.Add(review);
                    await _context.SaveChangesAsync();
                     
                    sql= "INSERT INTO game_review VALUES("+ reviewRequest.GameId+ ","+ review.Id+")";
                    var result3 = connection.Query<int>(sql);
                }
                
                return Ok(Message.ToJson("Review is created"));
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
