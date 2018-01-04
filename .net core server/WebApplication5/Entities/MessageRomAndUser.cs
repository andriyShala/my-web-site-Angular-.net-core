using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entities
{
    public class MessageRomAndUser
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int MessageRomId { get; set; }
        public MessageRom MessageRom { get; set; }
    }
}
