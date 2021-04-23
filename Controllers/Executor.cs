using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
        public IActionResult Post(string proc_name)
        {
            //TODO security tests
            // TODO multithreading
            object result;
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(proc_name, conn) { CommandType = CommandType.StoredProcedure };

            conn.Open();
            result = command.ExecuteScalar();
            //Console.WriteLine(result);
            conn.Close();

            //TODO tests

            //TODO try/catch
            return Ok(result);
        }
    }
}
