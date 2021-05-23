using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Authentication
{
    public class UserAuth
    {
        public string UserMail { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
    }
}
