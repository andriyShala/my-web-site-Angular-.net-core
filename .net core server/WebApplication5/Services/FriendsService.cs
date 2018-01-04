using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Dtos;
using WebApplication5.Entities;
using WebApplication5.Helpers;

namespace WebApplication5.Services
{
            public interface IFriendsService
            {
                IEnumerable<Friend> GetAllByUser(string username);
                 Friend GetById(int id);
                void CreateFriend(int propositionId,bool answer);
        void CreateProposition(string username1, string username2);
        List<Proposition> GetAllPropositionByUser(string username);
                void Delete(string username1, string username2);
        bool isSendProposition(string username1, string username2);
        bool isFriend(int usname1, string usname2);
    }

    public class FriendsService : IFriendsService
    {
        private DataContext _context;
        public FriendsService(DataContext context)
        {
            this._context = context;
        }
        public void CreateFriend(int propositionId, bool answer)
        {
            var proposition = _context.Propositions.FirstOrDefault(x => x.Id == propositionId);

            if (answer == true)
            {
                var friend = _context.Friends.Add(new Friend() { User1Id = proposition.OvnUserId, User2Id = proposition.User2Id, username1 = proposition.OvnUsername, username2 = proposition.Username2 });
            }
                _context.Propositions.Remove(proposition);

                _context.SaveChanges();
               
                
           
        }
        private bool isFriend(User user1,User user2)
        {
            var friend = _context.Friends.FirstOrDefault(x => x.User1Id == user1.Id && x.User2Id == user2.Id);
            if(friend==null)
            {
                return false;
            }
            return true;
        }
        public bool isFriend(int us1id,string usname2)
        {
            var user2 = _context.Users.FirstOrDefault(x => x.Username == usname2);

            var friend = _context.Friends.FirstOrDefault(x => x.User1Id == us1id && x.User2Id == user2.Id|| x.User1Id == user2.Id && x.User2Id == us1id);
            if (friend == null)
            {
                return false;
            }
            return true;

        }

        public void Delete(string username1, string username2)
        {
            var user1 = _context.Users.FirstOrDefault(x => x.Username == username1);
            var user2 = _context.Users.FirstOrDefault(x => x.Username == username2);
            if (user1 != null && user2 != null && isFriend(user1, user2) == true)
            {
                _context.Friends.Remove(_context.Friends.FirstOrDefault(x => x.User1Id == user1.Id && x.User2Id == user2.Id));
                _context.SaveChanges();
            }
        }

        public IEnumerable<Friend> GetAllByUser(string username)
        {
            var user1 = _context.Users.FirstOrDefault(x => x.Username == username);
            if(user1!=null)
            {
                return _context.Friends.Where(x => x.User1Id == user1.Id || x.User2Id == user1.Id);
            }
            return null;
        }

        public Friend GetById(int id)
        {
            return _context.Friends.FirstOrDefault(x => x.id == id);
        }

        public void CreateProposition(string ovnUserName, string username2)
        {
            var user1 = _context.Users.FirstOrDefault(x => x.Username == ovnUserName);
            var user2 = _context.Users.FirstOrDefault(x => x.Username == username2);
            if(user1!=null&&user2!=null)
            {
                _context.Propositions.Add(
                    new Proposition() {
                        OvnUserId = user1.Id,
                        User2Id = user2.Id,
                        OvnUsername = user1.Username,
                        Username2 = user2.Username
                    });
                _context.SaveChanges();

            }
        }
        

        List<Proposition> IFriendsService.GetAllPropositionByUser(string username)
        {
            return _context.Propositions.Where(x => x.Username2 == username).ToList();
          
        }

        public bool isSendProposition(string username1, string username2)
        {
            return _context.Propositions.Any(x => x.OvnUsername == username1 && x.Username2 == username2 || x.Username2 == username1 && x.OvnUsername == username2);
        }
    }
}
