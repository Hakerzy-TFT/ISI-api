
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
using Microsoft.Extensions.Logging;
using System.Text;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly alvorContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Users> _logger;

        public Users(alvorContext context, IConfiguration configuration, ILogger<Users> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EndUser>>> GetEndUsers()
        {
            _logger.Log(LogLevel.Information, "Called GetEndUsers()");
            return await _context.EndUsers.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EndUser>> GetEndUser(int id)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Called GetEndUsersby id: " + id);
                var endUser = await _context.EndUsers.FindAsync(id);

                if (endUser == null)
                {
                    return NotFound();
                }

                return endUser;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }

        [HttpGet("byemail/{email}")]
        public ActionResult<EndUser> GetEndUser(string email)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Called GetEndUsers()/ByEmail ({email})");

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
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndUser(int id, EndUser endUser)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Called PutEndUser with email: {endUser.Email}");

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
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }



        [HttpPut("/update-username")]
        public async Task<IActionResult> PutEndUserPassword(EndUserDto endUserDto)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Called PutEndUser with id: {endUserDto.Id}");
                if (!_context.EndUsers.Any(o => o.Id == endUserDto.Id))
                    return BadRequest("User does not exist!");
                if (_context.EndUsers.Any(o => o.Username == endUserDto.Username))
                    return BadRequest("Username exists");
                var user = new EndUser() { Id = endUserDto.Id, Username = endUserDto.Username };

                _context.EndUsers.Attach(user);
                _context.Entry(user).Property(x => x.Username).IsModified = true;

                await _context.SaveChangesAsync();
                return Ok("User updated!");
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }
        [HttpPut("/update-password")]
        public async Task<IActionResult> PutEndUserUsername(UserUpdatePassword userUpdatePassword)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Called PutEndUser with email: ");
                string sql = "select id from end_user_security where end_user_id = " + userUpdatePassword.EndUserId + "";
                if (!_context.EndUserSecurities.Any(o => o.EndUserId == userUpdatePassword.EndUserId))
                    return BadRequest("User does not exist!");
                using (SqlConnection connection = new(_context.Database.GetConnectionString()))
                {
                    var result = connection.Query<int>(sql);
                    Console.WriteLine(result.First());

                    var passwordManager = new PasswordManager();
                    var salt = passwordManager.GenerateSaltForPassowrd();
                    byte[] hashed = passwordManager.ComputePasswordHash(userUpdatePassword.Password, salt);
                    var endUserSecurities = new EndUserSecurity()
                    {
                        Id = result.First(),
                        EndUserId = userUpdatePassword.EndUserId,
                        Salt = salt,
                        HashedPassword = hashed,

                    };



                    _context.EndUserSecurities.Attach(endUserSecurities);

                    _context.Entry(endUserSecurities).State = EntityState.Modified;
                    //_context.Entry(fullEndUserSecurities).Property(x => x.HashedPassword).IsModified = true;
                    await _context.SaveChangesAsync();
                    return Ok("User's password updated!");
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EndUser>> PostEndUser(UserRegister userRegister)
        {
            string sql = "EXEC   gs_get_user_by_email @email= '" + userRegister.Email + "'";
            string sqlUsername = "EXEC   gs_get_user_by_username @username= '" + userRegister.Username + "'";
            try
            {
                _logger.Log(LogLevel.Information, $"Called PostEndUser()with email: ({userRegister.Email})");
                if (userRegister.Email == userRegister.Username)
                {
                    //Console.WriteLine(result.First());
                    return BadRequest("{\"result\" : \"The same data!\"}");
                }
                using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    var result = connection.Query<string>(sql);
                    var resultUsername = connection.Query<string>(sqlUsername);
                    //Console.WriteLine(result.First());

                    if (result.Any() || resultUsername.Any())
                    {
                        //Console.WriteLine(result.First());
                        return BadRequest("{\"result\" : \"user already exists!\"}");
                    }

                    else
                    {
                        //Console.WriteLine(result.First());
                        var user = new EndUser
                        {
                            Username = userRegister.Username,
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
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndUser(int id)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Delete end user ()with id: ({id})");

                var endUser = await _context.EndUsers.FindAsync(id);
                if (endUser == null)
                {
                    return NotFound();
                }

                _context.EndUsers.Remove(endUser);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }

        private bool EndUserExists(int id)
        {

            try
            {
                _logger.Log(LogLevel.Information, $"EndUserExists ()with id: ({id})");
                return _context.EndUsers.Any(e => e.Id == id);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("Coins")]
        public ActionResult<string> UpdateCoins([FromBody] UserCoinsDto request)
        {
            if (HakerzyLib.Core.Utils.IsAnyNullOrEmpty(request))
            {
                return BadRequest("Wrong input data!");
            }

            string sqlcmd = $"EXEC gs_update_user_coins @username='{request.Username}', @coins = {request.IncrementBalanceBy};";

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

        [HttpPost]
        [Route("login")]
        public ActionResult<string> Login([FromBody] UserAuth request)
        {
            // find user
            // check password
            // send jwt access token
            try
            {
                _logger.Log(LogLevel.Information, $"Login() with email: ({request.UserMail})");

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
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Exception thrown in runtiome - {e.Message}");
                throw;
            }
        }
    }
}