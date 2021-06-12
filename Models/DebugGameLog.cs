using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class DebugGameLog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? PostedDate { get; set; }
        public double? TotalRating { get; set; }
        public int? StudioId { get; set; }
        public int? GamePlatformId { get; set; }
        public int? GameTypeId { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public string StudioName { get; set; }
        public int? GamePageId { get; set; }
        public string ImgSrc { get; set; }
        public int? StatusId { get; set; }
    }
}
