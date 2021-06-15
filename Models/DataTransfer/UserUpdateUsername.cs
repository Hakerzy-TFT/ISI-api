using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class UserUpdateUsername
    {
        public int EndUserId { get; set; }
        public string Username { get; set; }
    }
}
