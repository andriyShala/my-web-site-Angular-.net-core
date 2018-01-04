using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Services;
using WebApplication5.Dtos;
using System.IdentityModel.Tokens.Jwt;
using WebApplication5.Helpers;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApplication5.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Controllers
{
    
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private IFriendsService _friendsService;
        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSetings,IFriendsService friendsService)
        {
            _friendsService = friendsService;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSetings.Value;
        }
    

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CurrentImage=user.CurrentImage,
                Token = tokenString
            });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            try
            {
                //save User
                _userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            string username = this.HttpContext.Request.Headers["username"];
            string searchPerson = this.HttpContext.Request.Headers["searchPerson"];
            if (searchPerson != null)
            {
                List<UserDto> list = new List<UserDto>();
                var user = _userService.GetByUsername(username);
                var users =_userService.GetAll(username, searchPerson);
                foreach (var item in users)
                {
                    list.Add(
                        new UserDto() {
                            CurrentImage = MessageService.GetImgUrlBy(HttpContext.Request.Host.Value, item.CurrentImage, item.Username)
                            , FirstName = item.FirstName,
                            LastName = item.LastName,
                            Username = item.Username,
                            IsFriend = _friendsService.isFriend(user.Id, item.Username).ToString(),
                            IsSendProposition = _friendsService.isSendProposition(username,item.Username).ToString()
                        });
                }
               return Ok(list);
            }
            else
            {
                var users = _userService.GetAll(username);
                var userDtos = _mapper.Map<IList<UserDto>>(users);
                foreach (var item in userDtos)
                {
                    item.Password = "";
                }

                return Ok(userDtos);
            }
            
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;
            try
            {
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
