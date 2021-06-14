using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class BugRequest
    {
        public string Title { set; get; }
        public string Description { set; get; }
        public int EndUserId { set; get; }
        public int GameId { set; get; }
    }
}
