using MessageToFuture.DTOs;
using MessageToFuture.Interfaces;
using MessageToFuture.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace MessageToFuture.Services
{
    public class MessageService : IMessageService
    {
        private readonly MessagesDbContext _context;
        const int DAYS_BEFORE_SEND = 2; 
        private readonly ILogger _logger;
        public MessageService(MessagesDbContext context,ILogger<MessageService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Message> AddMessageAsync(MessageDTO message)
        {
            DateTime deliveryDate = DateTime.UtcNow.AddDays(DAYS_BEFORE_SEND);
            Message messageToAdd = new Message { Content=message.Content, Title=message.Title, UserId=message.UserId,CreatedAt=DateTime.UtcNow, DeliveryDateTime=deliveryDate  };
            await _context.Messages.AddAsync(messageToAdd);
            await _context.SaveChangesAsync();
            return messageToAdd;
        }

        public async Task<Message> GetMessageByIdAsync(Guid messageId)
        {
            //await AddMessages();
            return await _context.Messages.FindAsync(messageId);
        }

        public async Task<Message> RemoveMessageAsync(Guid messageId)
        {
            Message message = await GetMessageByIdAsync(messageId);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<List<Message>> GetMessageToDelivery()
        {
            return await _context.Messages.Include(m=>m.User).Where(m => m.IsDelivered==false && m.DeliveryDateTime.Date >= DateTime.UtcNow.Date).ToListAsync();
        }

        public async Task UpdateMessages(List<Message> messages)
        {
            
            List<Message> messageToDelivery = await GetMessageToDelivery();
            _context.Messages.UpdateRange(messageToDelivery);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateMessage(Message message)
        {
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }



    }
}
