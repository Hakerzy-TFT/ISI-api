using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class UserProfileDto
    {
        public string Username { set; get; }
        public string Email { set; get; }
        public string Name { set; get; }
        public int UserLevel { set; get; }
        public string UserType { set; get; }

        public string IconSrc { set; get; }

        public List<ActivitiesDto> Activities {set; get;}
    }
}
