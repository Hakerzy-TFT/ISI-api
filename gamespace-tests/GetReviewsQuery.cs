using gamespace_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace gamespace_tests
{
    public class GetReviewsQuery
    {
        private alvorContext _context;

        public GetReviewsQuery(alvorContext context)
        {
            this._context = context;
        }

        public IList<Review> Execute()
        {
            return _context.Reviews
                .OrderBy(c => c.Rating)
                .ToList();
        }
    }
}