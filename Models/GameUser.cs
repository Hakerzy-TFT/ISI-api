using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class GameUser
    {
        public int Id { get; set; }
        public int? EndUserId { get; set; }

        public virtual EndUser EndUser { get; set; }
    }
}
