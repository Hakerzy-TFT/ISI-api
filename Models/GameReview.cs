using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class GameReview
    {
        public int Id { get; set; }
        public int? GameId { get; set; }
        public int? ReviewId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Review Review { get; set; }
    }
}
