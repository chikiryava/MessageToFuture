using MessageToFuture.DTOs;
using MessageToFuture.Interfaces;
using MessageToFuture.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageToFuture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO)
        {
            if (String.IsNullOrEmpty(userDTO.Name))
            {
                return BadRequest("Name is empty");
            }

            if (String.IsNullOrEmpty(userDTO.Email))
            {
                return BadRequest("Email is empty");
            }

            if (String.IsNullOrEmpty(userDTO.Password))
            {
                return BadRequest("Password is empty");
            }
            if (await _userService.GetByEmailAsync(userDTO.Email) != null)
            {
                return BadRequest("User with same email is exist");
            }
            var user = await _userService.AddUserAsync(userDTO);

            return Ok(user);


        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            User user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            User user = await _userService.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetUserMessages(Guid userId)
        {
            List<Message> userMessages = await _userService.GetUserMessagesAsync(userId);
            return Ok(userMessages);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> users = await _userService.GetUsersAsync();
            return Ok(users);
        }
    }
}
