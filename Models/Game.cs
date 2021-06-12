using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class Game
    {
        public Game()
        {
            GameKeys = new HashSet<GameKey>();
            GameReviews = new HashSet<GameReview>();
            GameUsers = new HashSet<GameUser>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? PostedDate { get; set; }
        public double TotalRating { get; set; }
        public string ImgSrc { get; set; }
        public int StudioId { get; set; }
        public int GamePageId { get; set; }
        public int StatusId { get; set; }
        public int GamePlatformId { get; set; }
        public int GameTypeId { get; set; }

        public virtual GamePage GamePage { get; set; }
        public virtual GamePlatform GamePlatform { get; set; }
        public virtual GameType GameType { get; set; }
        public virtual Status Status { get; set; }
        public virtual Studio Studio { get; set; }
        public virtual ICollection<GameKey> GameKeys { get; set; }
        public virtual ICollection<GameReview> GameReviews { get; set; }
        public virtual ICollection<GameUser> GameUsers { get; set; }
    }
}
