using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Dtos
{
    public class MessageRomDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime TimeLastMessage { get; set; }
        public List<UserDto> Users { get; set; }
        public string LastMessageText { get; set; }
    }
}
