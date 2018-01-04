using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entities
{
    public class User
    {
        public User()
{
           Images = new List<Image>();
            MessageAndUsers = new List<MessageRomAndUser>();
            Friends = new List<Friend>(); 
}
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
        public int CurrentImage { get; set; }
    public string Username { get; set; }
        public List<Image> Images { get; set; }
        public List<MessageRomAndUser> MessageAndUsers { get; set; }
    public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Friend> Friends { get; set; }
    }
}
