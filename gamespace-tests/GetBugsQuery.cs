using gamespace_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace gamespace_tests
{
    public class GetBugsQuery
    {
        private alvorContext _context;

        public GetBugsQuery(alvorContext context)
        {
            this._context = context;
        }

        public IList<Bug> Execute()
        {
            return _context.Bugs
                .OrderBy(c => c.Title)
                .ToList();
        }
    }
}