using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class EndUserSecurity
    {
        public int Id { get; set; }
        public int Salt { get; set; }
        public byte[] HashedPassword { get; set; }
        public int EndUserId { get; set; }

        public virtual EndUser EndUser { get; set; }
    }
}
