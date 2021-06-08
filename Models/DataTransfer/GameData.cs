using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class GameData
    {
        public string Title { set; get; }
        public string Description { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime PostedDate { set; get; }
        public string GameStudioName { set; get; }
        public string Platform { set; get; }
        public string Genre { set; get; }
        public string ImgSrc { set; get; }
        public int StatusId { set; get; }

        public string BackgroundColor { set; get; }
        public string Button1Url { set; get; }
        public string Button2Url { set; get; }
        public string GamePageDescription { set; get; }
        public string FontColor { set; get; }
        public string Header { set; get; }
        public string Img1Src { set; get; }
        public string Img2Src { set; get; }
        public string Img3Src { set; get; }
        public string BackgroundImage { set; get; }
    }
}
