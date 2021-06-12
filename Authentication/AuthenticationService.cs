using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using gamespace_api.Models;

namespace gamespace_api.Authentication
{
    public class AuthenticationService
    {
        public static string GenerateJwtToken(EndUser user, IConfiguration configuration)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(HakerzyLib.Security.JwtClaimTypes.uid, user.Id.ToString()),
                new Claim(HakerzyLib.Security.JwtClaimTypes.uname, user.Name),
                new Claim(HakerzyLib.Security.JwtClaimTypes.usurname, user.Surname),
                new Claim(HakerzyLib.Security.JwtClaimTypes.ubirthday, user.DateOfBirth.ToString()),
                new Claim(HakerzyLib.Security.JwtClaimTypes.utype, user.UserTypeId == 0 ? "admin" : "user" )
            };
            Console.WriteLine(configuration["jwt:Secret"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["jwt:ExpireDays"]));

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                configuration["jwt:ValidAudience"],
                configuration["jwt:ValidIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
