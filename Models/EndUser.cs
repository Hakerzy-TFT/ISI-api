using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class EndUser
    {
        public EndUser()
        {
            Bugs = new HashSet<Bug>();
            EndUserSecurities = new HashSet<EndUserSecurity>();
            GameUsers = new HashSet<GameUser>();
            Reviews = new HashSet<Review>();
            Studios = new HashSet<Studio>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Wallet { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual ICollection<Bug> Bugs { get; set; }
        public virtual ICollection<EndUserSecurity> EndUserSecurities { get; set; }
        public virtual ICollection<GameUser> GameUsers { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Studio> Studios { get; set; }
    }
}
