using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class GamePlatform
    {
        public GamePlatform()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Platform { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
