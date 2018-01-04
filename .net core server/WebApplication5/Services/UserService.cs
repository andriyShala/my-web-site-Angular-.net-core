using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication5.Entities;
using WebApplication5.Helpers;

namespace WebApplication5.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll(string username);
        IEnumerable<User> GetAll(string username, string serchPerson);
        User GetById(int id);
        User GetByUsername(string usname);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
    public class UserService : IUserService
    {
        private DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
           
        }

        public User Authenticate(string username, string password)
        {
            if(string.IsNullOrEmpty(username)||string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");
            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username" + user.Username + "is alredy taken");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if(user!=null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll(string username)
        {
            return _context.Users.Where(x=>x.Username!=username);
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);
            if (user == null)
                throw new AppException("User not found");
            if(userParam.Username!=user.Username)
            {
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username" + userParam.Username + "is alredy taken");
            }
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;
            if(!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        private static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            if(password==null)
            {
                throw new ArgumentNullException("password");
            }
            if(string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password,byte[] storedHash,byte[] storedSalt)
        {
            if(password==null)
            {
                throw new ArgumentException("password");
            }
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computerdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computerdHash.Length; i++)
                {
                    if(computerdHash[i]!=storedHash[i])
                    {
                        return false;
                    }
                }
             
            }
            return true;
        }

        public IEnumerable<User> GetAll(string username, string serchPerson)
        {
            return _context.Users.Where(x => x.Username != username && x.FirstName.Contains(serchPerson));
        }

        public User GetByUsername(string usname)
        {
            return _context.Users.FirstOrDefault(x => x.Username == usname);
        }
    }
}
