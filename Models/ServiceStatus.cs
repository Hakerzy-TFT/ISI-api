using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class ServiceStatus
    {
        public int ServiceStatusId { get; set; }
        public bool WebApp { get; set; }
        public bool Api { get; set; }
        public bool Db { get; set; }
    }
}
