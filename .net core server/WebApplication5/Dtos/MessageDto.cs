using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Dtos
{
    public class MessageDto
    {
        public int id { get; set; }
        public string Text { get; set; }
        public bool Self { get; set; }
        public DateTime TimePost { get; set; }
    }
}
