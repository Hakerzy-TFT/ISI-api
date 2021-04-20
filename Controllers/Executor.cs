using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Executor : ControllerBase
    {
        private readonly string connectionString;
        public Executor(IConfiguration configuration)
        {
            // wczytuje connection string z configuracji
            connectionString = configuration.GetValue<string>($"ConnectionStrings:{configuration.GetValue<string>("Setup:ActiveDatabase")}");
            // wyświetla w konsoli wczytanego connection stringa
            //Console.WriteLine($"connection string: {connectionString}");
        }

        //~Executor()
        //{

        //}
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok("oks");
        }
    }
}
