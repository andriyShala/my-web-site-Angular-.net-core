using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Controllers;
using WebApplication5.Dtos;
using WebApplication5.Entities;
using WebApplication5.Helpers;

namespace WebApplication5.Services
{
    public interface IMessageService
    {
        IEnumerable<Entities.Message> GetAll(int idrom);
        IEnumerable<MessageRom> GetAllRoms(string username);
        IEnumerable<MessageRomDto> GetAllRomsDto(string username,string host);
        List<Entities.Message> GetMessageByRom(int romid, string username);
        List<Entities.User> GetAllUsersByRom(int romid);
        MessageRom GetMessageRomByUsers(string username1, string username2);
        Entities.Message GetById(int id, string username);
        void Create(string _author, string toUsername, string text);
        void Update(Entities.Message message, string username);
        void Delete(int id, string username);
    }
    
    public class MessageService : IMessageService
        {

            private DataContext _context;
            public MessageService(DataContext context)
            {
                _context = context;
            }

        public void Create(string _author,string toUsername, string text)
        {
            DateTime dateTimeNow = DateTime.Now;
            Entities.Message message = new Entities.Message();
            MessageRom messageRom = null;

            var author= _context.Users.FirstOrDefault(x => x.Username == _author);
            var recipient = _context.Users.FirstOrDefault(x => x.Username == toUsername);

            var mesanduser = _context.MessageRomsAndUsers.Where(x => x.UserId == author.Id);
            foreach (var item in mesanduser)
            {
                var temp = _context.MessageRomsAndUsers.FirstOrDefault(x => x.UserId == recipient.Id && x.MessageRomId == item.MessageRomId);
                if(temp!=null)
                {
                    messageRom =_context.MessageRoms.FirstOrDefault(x=>x.id==temp.MessageRomId);
                    temp.MessageRom.LastMessageText = text;
                    temp.MessageRom.TimeLastMessage = dateTimeNow;
                    break;
                    
                }
            }



            if (messageRom != null)
            {
                message.MessageRom = messageRom;
                message.MessageRomId = messageRom.id;
                message.Text = text;
                message.TimePost = dateTimeNow;
                message.authorId = author.Id;
                message.recipientid = recipient.Id;
                messageRom.Messages.Add(_context.Messages.Add(message).Entity);
                messageRom.TimeLastMessage = dateTimeNow;
                messageRom.LastMessageText = text;
                _context.SaveChanges();
            }
            else
            {
                var mesrom = _context.MessageRoms.Add(new MessageRom() { Name = "" ,LastMessageText=text,TimeLastMessage=dateTimeNow});
                
                _context.SaveChanges();
                var mesromanduser = _context.MessageRomsAndUsers.Add(new MessageRomAndUser() { MessageRom = mesrom.Entity, MessageRomId = mesrom.Entity.id, User = author, UserId = author.Id });
                var mesromanduser1 = _context.MessageRomsAndUsers.Add(new MessageRomAndUser() { MessageRom = mesrom.Entity, MessageRomId = mesrom.Entity.id, User = recipient, UserId = recipient.Id });
                _context.SaveChanges();
                mesrom.Entity.MessageRomAndUsers.Add(mesromanduser.Entity);
                mesrom.Entity.MessageRomAndUsers.Add(mesromanduser1.Entity);
                _context.SaveChanges();
                message.MessageRom = mesrom.Entity;
                message.MessageRomId = mesrom.Entity.id;
                message.Text = text;
                message.TimePost = dateTimeNow;
                message.authorId = author.Id;
                message.recipientid = recipient.Id;
                mesrom.Entity.Messages.Add(_context.Messages.Add(message).Entity);
                var ff = ChatWebSocketMiddleware.GetSocketByUsername(recipient.Username);
                string messagee = JsonConvert.SerializeObject(
                    new MessageRomDto() {
                        id = mesrom.Entity.id,
                        LastMessageText = mesrom.Entity.LastMessageText,
                        Name = "",
                        TimeLastMessage = mesrom.Entity.TimeLastMessage,
                        Users = new List<UserDto>() {
                            new UserDto() {
                                CurrentImage =MessageService.GetImgUrlBy(ImageController.hoststring,
                                 author.CurrentImage,author.Username),
                            FirstName=author.FirstName,
                            Username=author.Username,
                            Id=author.Id,
                            IsFriend=true.ToString(),
                             LastName=author.LastName,
                             IsSendProposition=false.ToString()
                            } }

                    });
                ChatWebSocketMiddleware.SendStringAsync(ff.Websocket, new Helpers.Message() { author=_author,recipient=recipient.Username,type=Helpers.MessagesType.RomCreate.Value,message= messagee}.ToString(), ff.cancellationToken);
                _context.SaveChanges();

            }

        }

        public void Delete(int id, string username)
        {
        }

        public IEnumerable<Entities.Message> GetAll(int idrom)
        {
            return null;
        }

        public IEnumerable<MessageRom> GetAllRoms(string username)
        {
            List<MessageRom> list = new List<MessageRom>();
            var user = _context.Users.FirstOrDefault(x => x.Username == username);
            foreach (var item in _context.MessageRomsAndUsers.Where(x=>x.UserId==user.Id))
            {
                list.Add(_context.MessageRoms.FirstOrDefault(x=>x.id==item.id));
            }
            return list;
        }

        public IEnumerable<MessageRomDto> GetAllRomsDto(string username,string host)
        {
            List<MessageRomDto> list = new List<MessageRomDto>();
            var user = _context.Users.FirstOrDefault(x => x.Username == username);
            foreach (var item in _context.MessageRomsAndUsers.Where(x => x.UserId == user.Id))
            {
                var mess = _context.MessageRoms.FirstOrDefault(x => x.id == item.MessageRomId);
               
                List<UserDto> uslist = new List<UserDto>();
                foreach (var us in GetAllUsersByRom(item.MessageRomId))
                {
                    string imageUrl =GetImgUrlBy(host,us.CurrentImage,us.Username);
                    if (us.Username != username)
                        uslist.Add(new UserDto() { CurrentImage = imageUrl, FirstName = us.FirstName, Id = us.Id, LastName = us.LastName, Username = us.Username });
                }
                list.Add(new MessageRomDto() { id=mess.id,LastMessageText=mess.LastMessageText,Name=mess.Name,TimeLastMessage=mess.TimeLastMessage,Users= uslist });
            }
            return list;
        }
       
        public static string GetImgUrlBy(string host,int curentImage,string usname)
        {
            return String.Format("http://{0}/api/Image/?image={1},{2}", host, curentImage, usname);
        }
        public List<User> GetAllUsersByRom(int romid)
        {
            List<User> list = new List<User>();
            foreach (var item in _context.MessageRomsAndUsers.Where(x=>x.MessageRomId==romid))
            {
                list.Add(_context.Users.FirstOrDefault(x=>x.Id==item.UserId));
            }
            return list;
        }

        public Entities.Message GetById(int id, string username)
        {

            return null;

        }

        public List<Entities.Message> GetMessageByRom(int romid,string username)
        {
            var merRom = _context.MessageRoms.FirstOrDefault(x => x.id == romid);
            if(merRom!=null)
            {
                var user = _context.Users.FirstOrDefault(x => x.Username == username);
               if(null!=_context.MessageRomsAndUsers.FirstOrDefault(x => x.MessageRomId == romid && x.User.Username == username))
                {
                    return _context.Messages.Where(x => x.MessageRomId == merRom.id).ToList();

                }
                return null;

            }
            else
            {
                return null;
            }
        }

        public MessageRom GetMessageRomByUsers(string username1, string username2)
        {
            var user1 = _context.Users.FirstOrDefault(x => x.Username == username1);
            var user2 = _context.Users.FirstOrDefault(x => x.Username == username2);

            var mesandrom = _context.MessageRomsAndUsers.Where(x => x.UserId == user1.Id);
            var messadrom2= _context.MessageRomsAndUsers.Where(x => x.UserId == user2.Id);

            foreach (var item in mesandrom)
            {
                var messageRom = messadrom2.FirstOrDefault(x => x.MessageRomId == item.MessageRomId);
                if(messadrom2!=null)
                {
                    return messageRom.MessageRom;
                }
            }
            return null;

        }

        public void Update(Entities.Message message, string username)
        {
            
        }
    }
}
