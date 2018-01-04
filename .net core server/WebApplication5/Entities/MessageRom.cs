using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entities
{
    public class MessageRom
    {
        public MessageRom()
        {
            this.MessageRomAndUsers = new List<MessageRomAndUser>();
            this.Messages = new List<Message>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public List<MessageRomAndUser> MessageRomAndUsers { get; set; }
        public List<Message> Messages { get; set; }
        public DateTime TimeLastMessage { get; set; }
        public string LastMessageText { get; set; }

    }
}
