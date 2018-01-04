using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string CurrentImage { get; set; }
        public string Password { get; set; }
        public string IsFriend { get; set; }
        public string IsSendProposition { get; set; }
    }
}
