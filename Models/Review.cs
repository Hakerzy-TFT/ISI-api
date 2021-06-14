using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class Review
    {
        public Review()
        {
            GameReviews = new HashSet<GameReview>();
        }

        public int Id { get; set; }
        public double Rating { get; set; }
        public string ReviewContent { get; set; }
        public int EndUserId { get; set; }
        public int StatusId { get; set; }

        public virtual EndUser EndUser { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<GameReview> GameReviews { get; set; }
    }
}
