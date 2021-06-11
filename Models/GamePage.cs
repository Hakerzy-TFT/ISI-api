using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class GamePage
    {
        public GamePage()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundColor { get; set; }
        public string Img1Src { get; set; }
        public string Img2Src { get; set; }
        public string Img3Src { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Button1Url { get; set; }
        public string Button2Url { get; set; }
        public string ButtonColor { get; set; }
        public string FontColor { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
