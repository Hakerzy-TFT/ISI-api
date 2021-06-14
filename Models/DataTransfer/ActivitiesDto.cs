using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class ActivitiesDto
    {
        public string ActivityTitle { set; get; }
        public string Day { set; get; }
        public string Time { set; get; }
        public string IssueTitle { set; get; }
        public string Review { set; get; }
        //public bool IsRolled { set; get; }
        //public string? RolledDescription { set; get; }
        public string TargetGame { set; get; }
    }
}
