using System;
using System.Collections.Generic;

#nullable disable

namespace gamespace_api.Models
{
    public partial class Bug
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int EndUserId { get; set; }
        public int StatusId { get; set; }

        public virtual EndUser EndUser { get; set; }
        public virtual Status Status { get; set; }
    }
}
