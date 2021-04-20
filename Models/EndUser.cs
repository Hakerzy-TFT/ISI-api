using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class EndUser
    {
        public EndUser()
        {
            GameUsers = new HashSet<GameUser>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual ICollection<GameUser> GameUsers { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
