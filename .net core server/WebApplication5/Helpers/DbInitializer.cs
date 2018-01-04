using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Entities;
using WebApplication5.Services;

namespace WebApplication5.Helpers
{
    public static class DbInitializer
    {

        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (!context.Users.Any())
            {
                UserService userService = new UserService(context);
                foreach (var item in JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("MOCK_DATA.json")))
                {
                    userService.Create(item, "1111");
                }
              
                userService.Create(new User() { FirstName = "andriy", LastName = "shala", Username = "shala97",CurrentImage=1 }, "1234");
                userService.Create(new User() { FirstName = "admin", LastName = "admin", Username = "admin" ,CurrentImage=2}, "1111");

            }
            if (!context.Images.Any())
            {

                byte[] bt = File.ReadAllBytes("man.png");

                ImageService imageService = new ImageService(context);
                imageService.Create(new Image() { bytes = bt, IdUser = 1, Name = "das", type = MimeTypeMap.GetMimeType(".png") }, "shala97");
                imageService.Create(new Image() { bytes = bt, IdUser = 2, Name = "das", type = MimeTypeMap.GetMimeType(".png") }, "admin");
            }


        }




    }
    }
