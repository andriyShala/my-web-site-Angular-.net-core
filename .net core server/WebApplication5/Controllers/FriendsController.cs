using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Dtos;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    [Authorize]
    [Route("api/Friends")]
    public class FriendsController : Controller
    {
        private IFriendsService _friendsService;
        private IUserService _userService;
        public FriendsController(IFriendsService friendsService, IUserService userService)
        {
            this._friendsService = friendsService;
            this._userService = userService;
        }
        // GET: api/Friends
        [HttpGet]
        public IActionResult Get()
        {
            List<Object> list = new List<Object>();
            string usname = this.HttpContext.Request.Headers["Username"];
            string isproposition = this.HttpContext.Request.Headers["isProposition"];
            if(usname!=null&&isproposition==null)
            {
                foreach (var item in _friendsService.GetAllByUser(usname))
                {
                    Entities.User friend = new Entities.User();
                    if(item.username1!=usname)
                    {
                        friend = _userService.GetById(item.User1Id);
                    }
                    else
                    {
                        friend = _userService.GetById(item.User2Id);
                    }

                    list.Add(new UserDto()
                    {
                        CurrentImage = MessageService.GetImgUrlBy(HttpContext.Request.Host.Value, friend.CurrentImage, friend.Username),
                        Username = friend.Username,
                        FirstName = friend.FirstName,
                        IsFriend = true.ToString(),
                        LastName = friend.LastName
                    });
                } 
            }else if(isproposition!=null)
            {

                foreach (var item in _friendsService.GetAllPropositionByUser(usname))
                {
                    var prop= _userService.GetById(item.OvnUserId);
                    list.Add(new{
                        CurrentImage = MessageService.GetImgUrlBy(HttpContext.Request.Host.Value, prop.CurrentImage, prop.Username),
                        prop.Username,
                        prop.FirstName,
                        prop.LastName,
                        idproposition=item.Id
                    });
                }
            }


            return Ok(list);
        }



        [HttpPost]
        public IActionResult Post()
        {
            string usname = this.HttpContext.Request.Headers["Username"];
            string propuser = this.HttpContext.Request.Headers["Propuser"];
            if(usname!=null&&propuser!=null)
            _friendsService.CreateProposition(usname, propuser);
            return Ok();

        }

        [HttpPut]
        public void Put()
        {
            string usname = this.HttpContext.Request.Headers["Username"];
            string idProposition = this.HttpContext.Request.Headers["idProposition"];
            string answer = this.HttpContext.Request.Headers["answer"];
            if(usname!=null&&idProposition!=null&&answer!=null)
            {
                _friendsService.CreateFriend(Convert.ToInt32(idProposition), Convert.ToBoolean(answer));
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
