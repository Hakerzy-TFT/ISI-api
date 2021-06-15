using gamespace_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamespace_tests
{
    public class TestHelper
    {
        private readonly alvorContext _context;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<alvorContext>()
            .UseInMemoryDatabase(databaseName: "alvor")
            .Options;

            _context = new alvorContext(builder);
            // Delete existing db before creating a new one
            //_context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
