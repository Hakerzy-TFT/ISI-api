using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class GameDto
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public DateTime? ReleaseDate { set; get; }
        public DateTime? PostedDate { set; get; }
        public double TotalRating {set;get; }
        public string ImgSrc { set; get; }
        public string Img1Src { set; get; }
        public string Img2Src { set; get; }
        public string Img3Src { set; get; }
        public string Header { set; get; }
        public string FontColor { set; get; }
        public string ButtonColor { set; get; }
        public string StudioName { set; get; }
    }
}
