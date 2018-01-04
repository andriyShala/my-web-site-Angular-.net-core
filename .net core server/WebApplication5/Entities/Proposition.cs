using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entities
{
    public class Proposition
    {
        public int Id
        {
            get; set;
        }
        public int OvnUserId { get; set; }
        public string OvnUsername { get; set; }
        public int User2Id { get; set; }
        public string Username2 { get; set; }
    }
}
