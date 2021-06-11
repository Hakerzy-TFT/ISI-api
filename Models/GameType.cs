using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class GameType
    {
        public GameType()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
