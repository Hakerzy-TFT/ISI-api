using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class RankingResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? PostedDate { get; set; }
        public double? TotalRating { get; set; }
        public string ImgSrc { get; set; }
        public string Studio { get; set; }
        public int? GamePageId { get; set; }
        public string Status { get; set; }
        public string Platform { get; set; }
        public string Genre { get; set; }
    }
}
