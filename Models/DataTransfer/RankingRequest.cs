using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class RankingRequest
    {
        public string Criterium {set; get;}
        public string Platform { set; get; }
        public string Genre { set; get; }
    }
}
