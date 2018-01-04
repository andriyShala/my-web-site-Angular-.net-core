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
    [Route("api/Roms")]
    public class RomsController : Controller
    {
       
        private IMessageService _messageService;
        public RomsController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        // GET: api/Roms
        [HttpGet]
        public IActionResult Get()
        {
            string userna = this.HttpContext.Request.Headers["Username"];
            string romid = this.HttpContext.Request.Headers["id"];
            List<MessageRomDto> list = new List<MessageRomDto>();
            try
            {
                if (romid != null)
                {
                    foreach (var item in _messageService.GetAllUsersByRom(Convert.ToInt32(romid)))
                    {
                        if (item.Username != userna)
                        {
                            return Ok(new UserDto() { CurrentImage = "", Username = item.Username, FirstName = item.FirstName, LastName = item.LastName });
                        }
                    }

                }
                else
                {
                    var lis = _messageService.GetAllRomsDto(userna, HttpContext.Request.Host.Value);
                    return Ok(lis);
                }
            }
            catch
            {


                return NotFound();

            }
            return NotFound();


        }

        // GET: api/Roms/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Roms
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Roms/5
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
