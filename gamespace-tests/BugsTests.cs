using gamespace_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Linq;

namespace gamespace_tests
{
    public class BugsTests
    {
        [Fact]
        public void GetAllBugs()
        {
            var options = new DbContextOptionsBuilder<alvorContext>()
                .UseInMemoryDatabase(databaseName: "GetAllBugs")
                .Options;

            var _context = new alvorContext(options);
            Seed(_context);

            var query = new GetBugsQuery(_context);
            var result = query.Execute();

            Assert.Equal(3, result.Count);
        }
        [Fact]
       public void GetBugsById()
        {
            var options = new DbContextOptionsBuilder<alvorContext>()
                .UseInMemoryDatabase(databaseName: "GetBugsById")
                .Options;

            var _context = new alvorContext(options);
            Seed(_context);

            var query = new GetBugsQuery(_context);
            var result = query.Execute();

            Assert.Equal(2, result.ElementAt(2).Id);
        }
        private void Seed(alvorContext context)
        {
            var bugs = new[]
            {
                new Bug { Id = 1,Title = "sth1"},
                new Bug { Id = 3, Title = "sth2"},
                new Bug { Id = 2, Title = "sth3"}
            };
            context.Bugs.AddRange(bugs);
            context.SaveChanges();
        }
    }
}
