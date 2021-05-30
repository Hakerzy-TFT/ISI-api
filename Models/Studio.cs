using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public int? EndUserId { get; set; }

        public virtual EndUser EndUser { get; set; }
    }
}
