using MessageToFuture.DTOs;
using MessageToFuture.Interfaces;
using MessageToFuture.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageToFuture.Services
{
    public class UserService : IUserService
    {
        private readonly MessagesDbContext _context;
        public UserService(MessagesDbContext context)
        {
            _context = context;   
        }
        public async Task<User> AddUserAsync(UserDTO userDTO)
        {
            User userToAdd = new() { Name = userDTO.Name, Email = userDTO.Email, Password = userDTO.Password };
            await _context.AddAsync(userToAdd);
            await _context.SaveChangesAsync();
            return userToAdd;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }


        public async Task<List<Message>> GetUserMessagesAsync(Guid userId)
        {
            return await _context.Messages.Where(m=> m.UserId== userId).ToListAsync();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

    }
}
