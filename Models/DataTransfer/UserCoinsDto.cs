using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class UserCoinsDto
    {
        public string Username { set; get; }
        public int IncrementBalanceBy { set; get; }
    }
}
