using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entities
{
    public class Friend
    {
        public int id
        {
            get;set;
        }
        public int User1Id { get; set; }
        public string username1 { get; set; }
        public int User2Id { get; set; }
        public string username2 { get; set; }

    }
}
