using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int MessageRomId { get; set; }
        public int authorId { get; set; }
        public int recipientid { get; set; }
        public MessageRom MessageRom { get; set; }
        public string Text { get; set; }
        public DateTime TimePost { get; set; }
    }
}
