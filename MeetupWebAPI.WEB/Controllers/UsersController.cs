using AutoMapper;
using MeetupWebAPI.BLL.DTO.User;
using MeetupWebAPI.BLL.Interfaces;
using MeetupWebAPI.DAL.Entities;
using MeetupWebAPI.DAL.Interfaces;
using MeetupWebAPI.WEB.Models.Requests;
using MeetupWebAPI.WEB.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace MeetupWebAPI.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMeetupRepository _meetupRepo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepo, IMeetupRepository meetupRepo, IUserService userService, IMapper mapper)
        {
            _userRepo = userRepo;
            _meetupRepo = meetupRepo;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            User user = null;

            if (await _userRepo.HasUser(registerRequest.UserName))
            {
                return BadRequest("UserName is already taken!!!");
            }

            using (var hma5 = new HMACSHA512())
            {
                user = new User
                {
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    UserName = registerRequest.UserName,
                    PasswordHash = hma5.ComputeHash(Encoding.UTF8.GetBytes(registerRequest.Password)),
                    PasswordSalt = hma5.Key
                };

                _userRepo.CreateUser(user);
                await _userRepo.SaveAsync();
            }

            var response = new RegisterResponse(user);

            return Ok(response);
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Login(AuthenticateRequest loginRequest)
        {
            var user = await _userRepo.GetUserByName(loginRequest.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            using (var hma5 = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hma5.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                    {
                        return Unauthorized("Invalid password");
                    }
                }
            }

            var token = _userService.GenerateJwtToken(user);
            var response = new AuthenticateResponse(user, token);
            return Ok(response);
        }

        [HttpPost("JoinMeetup")]
        public async Task<ActionResult<UserDTO>> JoinMeetup(int meetupId)
        {
            var user = HttpContext.Items["User"] as User;
            if (user == null)
            {
                return Unauthorized("You are not authorized!");
            }
            var meetup = await _meetupRepo.GetMeetupById(meetupId);
            if (meetup == null)
            {
                return NotFound();
            }
            if (_userRepo.HasMeetup(user, meetup))
            {
                return BadRequest("You are already registered for this meetup. Please choose another");
            }
            _userRepo.AttachMeetupToUser(user, meetup);
            await _userRepo.SaveAsync();
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

    }
}
