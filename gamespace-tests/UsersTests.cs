using gamespace_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace gamespace_tests
{
    public class UsersTests : IDisposable
    {
        public  alvorContext _context;

        public UsersTests(alvorContext _context)
        {
            var builder = new DbContextOptionsBuilder<alvorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context.Database.EnsureCreated();
            _context = new alvorContext(builder);

            var users = new[]
            {
                new EndUser {Id = 1, Email="example@example.com", Username="uname"},
                new EndUser {Id = 2, Email="example2@example.com", Username="uname2"},
                new EndUser {Id = 3, Email="example3@example.com", Username="uname3"},
                new EndUser {Id = 4, Email="example4@example.com", Username="uname4"},
                new EndUser {Id = 5, Email="example5@example.com", Username="uname5"},
                new EndUser {Id = 6, Email="example6@example.com", Username="uname6"}
            };
            _context.EndUsers.AddRange(users);
            _context.SaveChanges();
            
        }
        //[Fact]
        //public async Task DeleteUser_ReturnsOk_WhenUserDeleted()
        //{
        //    var controller = new gamespace_api.Controllers.Users(_context);
        //    var result = await controller.DeleteEndUser(2);
        //    Assert.IsType<OkObjectResult>(result);
        //}

        //[Fact]
        //public async Task GetUsers_ReturnsOk()
        //{
        //    var controller = new gamespace_api.Controllers.Users(_context);
        //    var result = await controller.GetEndUser(2);
        //    Assert.IsType<OkObjectResult>(result);

        //}

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        

    }
}
