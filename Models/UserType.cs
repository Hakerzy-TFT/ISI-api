using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class UserType
    {
        public UserType()
        {
            EndUsers = new HashSet<EndUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EndUser> EndUsers { get; set; }
    }
}
