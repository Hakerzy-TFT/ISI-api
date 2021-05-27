  
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
using HakerzyLib.core;
using gamespace_api.Models.DataTransfer;
using HakerzyLib.Security;
using Newtonsoft.Json;
using System.Threading;
using Microsoft.Extensions.Configuration;
using gamespace_api.Authentication;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly alvorContext _context;
        private readonly IConfiguration _configuration;

        public Users(alvorContext context, IConfiguration configuration)
        {
            _configuration = configuration;
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

        [HttpGet("byemail/{email}")]
        public ActionResult<EndUser> GetEndUser(string email)
        {
            if (email is null)
            {
                return BadRequest(Message.ToJson("Empty parameter!"));
            }

            string sql = "EXEC gs_get_user_by_email @email= '" + email + "'";


            using (SqlConnection connection = new(_context.Database.GetConnectionString()))
            {
                var result = connection.Query<string>(sql);

                if (result.Any())
                {
                    return Ok(result.First().ToString());
                }
                else
                {
                    return BadRequest(Message.ToJson("user not found!"));
                }
            }
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
        public async Task<ActionResult<EndUser>> PostEndUser(UserRegister userRegister)
        {
            string sql = "EXEC   gs_get_user_by_email @email= '" + userRegister.Email + "'";
            try
            {
                using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    var result = connection.Query<string>(sql);
                    //Console.WriteLine(result.First());
                    if (result.Any())
                    {
                        //Console.WriteLine(result.First());
                        return BadRequest("{\"result\" : \"user already exists!\"}");
                    }

                    else
                    {
                        //Console.WriteLine(result.First());
                        var user = new EndUser
                        {
                            Email = userRegister.Email,
                            Name = userRegister.Name,
                            Surname = userRegister.Surname,
                            UserTypeId = userRegister.UserTypeId
                        };
                        _context.EndUsers.Add(user);
                        await _context.SaveChangesAsync();

                        var passwordManager = new PasswordManager();
                        var salt = passwordManager.GenerateSaltForPassowrd();
                        byte[] hashed = passwordManager.ComputePasswordHash(userRegister.Password, salt);

                        var res2 = connection.Query<string>(sql);
                        var userData = JsonConvert.DeserializeObject<List<EndUser>>(res2.First());
                        Console.WriteLine(userData.First().Id);

                        _context.EndUserSecurities.Add(new EndUserSecurity
                        {
                            Salt = salt,
                            HashedPassword = hashed,
                            EndUserId = userData.First().Id

                        }
                        );
                        await _context.SaveChangesAsync();
                    }
                    return Ok(Message.ToJson("User is created"));
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("error");
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

        [HttpPost]
        [Route("login")]
        public ActionResult<string> Login([FromBody] UserAuth request)
        {
            // find user
            // check password
            // send jwt access token

            var user = _context.EndUsers.FirstOrDefault(u => u.Email == request.UserMail);

            if (user == null)
            {
                //logger error
                return BadRequest(Message.ToJson("user doesnt exist!"));
            }

            EndUserSecurity userSecurities = _context.EndUserSecurities.FirstOrDefault(s => s.EndUserId == user.Id);

            if (userSecurities == null)
            {
                //logger error
                return BadRequest(Message.ToJson("user securities doesnt exist!"));
            }

            PasswordManager pm = new();

            if (pm.IsPassowrdValid(request.Password, (int)userSecurities.Salt, userSecurities.HashedPassword) == false)
            {
                //logger error
                return BadRequest(Message.ToJson("Bad password"));
            }

            var token = AuthenticationService.GenerateJwtToken(user, _configuration);

            return Ok(token);
        }
    }
}