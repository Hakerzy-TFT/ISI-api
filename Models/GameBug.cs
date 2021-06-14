using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class GameBug
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int BugId { get; set; }

        public virtual Bug Bug { get; set; }
        public virtual Game Game { get; set; }
    }
}
