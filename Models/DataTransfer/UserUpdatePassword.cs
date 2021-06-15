using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class UserUpdatePassword
    {
        public int EndUserId { get; set; }
        public string Password { get; set; }
    }
}
