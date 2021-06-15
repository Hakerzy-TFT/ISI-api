using gamespace_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Linq;

namespace gamespace_tests
{
    public class ReviewTests
    {
        [Fact]
        public void GetAllReviews()
        {
            var options = new DbContextOptionsBuilder<alvorContext>()
                .UseInMemoryDatabase(databaseName: "GetAllReviews")
                .Options;

            var _context = new alvorContext(options);
            Seed(_context);

            var query = new GetReviewsQuery(_context);
            var result = query.Execute();

            Assert.Equal(3, result.Count);
        }
        [Fact]
        public void GetReviewsById()
        {
            var options = new DbContextOptionsBuilder<alvorContext>()
                .UseInMemoryDatabase(databaseName: "GetReviewsById")
                .Options;

            var _context = new alvorContext(options);
            Seed(_context);

            var query = new GetReviewsQuery(_context);
            var result = query.Execute();

            Assert.Equal(1, result.ElementAt(1).Id);
        }
        private void Seed(alvorContext context)
        {
            var reviews = new[]
            {
                new Review { Id=1, Rating = 8.5},
                new Review { Id=2, Rating = 7.3},
                new Review { Id=3,  Rating = 10 }
            };
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
        }
    }
}
