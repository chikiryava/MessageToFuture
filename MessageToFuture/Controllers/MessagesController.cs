using MessageToFuture.DTOs;
using MessageToFuture.Interfaces;
using MessageToFuture.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageToFuture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessagesController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
           
        }

        [HttpGet("GetMessageById")]
        public async Task<IActionResult> GetMessageById(Guid id)
        {
            Message message = await _messageService.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpPost("AddMessage")]
        public async Task<IActionResult> AddMessage(MessageDTO messageDTO)
        {
            if (String.IsNullOrEmpty(messageDTO.Title))
            {
                return BadRequest("Title is empty");
            }
            if (String.IsNullOrEmpty(messageDTO.Content))
            {
                return BadRequest("Content is empty");
            }
            if(await _userService.GetByIdAsync(messageDTO.UserId) == null)
            {
                return BadRequest("This user is not exist");
            }
            Message message = await _messageService.AddMessageAsync(messageDTO);
            return Ok(message);
        }

   
    }
}
