using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class Studio
    {
        public Studio()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public int? EndUserId { get; set; }
        public int StudioPageId { get; set; }

        public virtual EndUser EndUser { get; set; }
        public virtual StudioPage StudioPage { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
