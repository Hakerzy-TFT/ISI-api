using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Models.DataTransfer
{
    public class EndUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Wallet { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string UserType { get; set; }
    }
}
