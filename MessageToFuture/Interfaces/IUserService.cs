using MessageToFuture.DTOs;
using MessageToFuture.Models;

namespace MessageToFuture.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> AddUserAsync(UserDTO userDTO);
        Task<List<Message>> GetUserMessagesAsync(Guid userId);
        Task<List<User>> GetUsersAsync();
    }
}
