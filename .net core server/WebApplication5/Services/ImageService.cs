using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Entities;
using WebApplication5.Helpers;
using System.IO;

namespace WebApplication5.Services
{
    public interface IImageService
    {
        IEnumerable<Image> GetAll();
        Image GetById(int id,string username);
        void Create(Image image, string username);
        void Update(Image image, string username);
        void Delete(int id,string username);
    }
    public class ImageService : IImageService
    {
        private DataContext _context;
        public ImageService(DataContext context)
        {
            _context = context;
            //if(_context.Images.Count()==0)
            //{
            //    byte[] bt = File.ReadAllBytes(@"C:\Users\Андрій\Pictures\3310.0.jpg");

            //    var user = _context.Users.FirstOrDefault(x => x.Username == "shala97");
            //    if (user != null)
            //    {
            //        _context.Images.Add(new Image() { bytes = bt, IdUser = user.Id, Name = "das", type = MimeTypeMap.GetMimeType("asd.jpg"), User = user });
            //    }
            //}
        }
       
        public void Create(Image image, string username)
        {
            if(String.IsNullOrWhiteSpace(username))
            {
                throw new AppException("Username is required");
            }

            var user = _context.Users.FirstOrDefault(x => x.Username == username);

            if(user==null)
            {
                throw new AppException("Username is not existed");
            }

            image.User = user;
            image.IdUser = user.Id;

            var img = _context.Images.Add(image);
            _context.SaveChanges();
           
            user.Images.Add(img.Entity);
            _context.SaveChanges();

       



        }

        public void Delete(int id,string username)
        {
            
        }

        public IEnumerable<Image> GetAll()
        {
          return _context.Images;
        }

        public Image GetById(int id, string username)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username);


            var img = _context.Images.FirstOrDefault(x => x.IdUser == user.Id);

            if (img == null)
            {
                return new Image() { bytes = File.ReadAllBytes("Sign-Error-icon.png"), id = 1, Name = "Error", type = "image/png"};
            }
            return img; 
        }

        public void Update(Image image, string username)
        {
            throw new NotImplementedException();
        }
    }
}
