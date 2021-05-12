using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class Log
    {
        public int LogsId { get; set; }
        public string LogMsg { get; set; }
        public DateTime? LogDate { get; set; }
    }
}
