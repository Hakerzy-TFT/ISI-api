using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class Game
    {
        public Game()
        {
            GameReviews = new HashSet<GameReview>();
            GameUsers = new HashSet<GameUser>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime PostedDate { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }
        public virtual ICollection<GameReview> GameReviews { get; set; }
        public virtual ICollection<GameUser> GameUsers { get; set; }
    }
}
