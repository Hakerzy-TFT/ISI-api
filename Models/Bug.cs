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
    }
}
