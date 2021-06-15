using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class GameKey
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int GameId { get; set; }
        public int? EndUserId { get; set; }

        public virtual EndUser EndUser { get; set; }
        public virtual Game Game { get; set; }
    }
}
