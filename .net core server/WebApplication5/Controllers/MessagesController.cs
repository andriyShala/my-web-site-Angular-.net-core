using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication5.Dtos;
using WebApplication5.Helpers;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
     [Authorize]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        // GET: api/Messages
        private IMessageService _messageService;
        private IUserService _userService;
        public MessagesController(IMessageService messageService,IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        // GET: api/Messages/5
        [HttpGet]
        public IActionResult Get()
        {
            List<MessageDto> list = new List<MessageDto>();
            try
            {
                string usname = this.HttpContext.Request.Headers["Username"];
                foreach (var item in _messageService.GetMessageByRom(Convert.ToInt32(this.HttpContext.Request.Headers["RomId"]), this.HttpContext.Request.Headers["Username"]))
                {
                    if(_userService.GetById(item.authorId).Username== usname)
                        list.Add(new MessageDto() { id = item.id,Self=true, Text = item.Text, TimePost = item.TimePost });
                    else
                        list.Add(new MessageDto() { id = item.id, Self = false, Text = item.Text, TimePost = item.TimePost });

                }
                return Ok(list);
            }
            catch
            {
                list.Add(new MessageDto() { id = 1, TimePost = DateTime.Now, Text = "asdasdas" });
                return Ok(list);

            }

        }
        
        // POST: api/Messages
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
