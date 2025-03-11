using MessageToFuture.DTOs;
using MessageToFuture.Models;

namespace MessageToFuture.Interfaces
{
    public interface IMessageService
    {
        Task<Message> GetMessageByIdAsync(Guid messageId);
        Task<Message> AddMessageAsync(MessageDTO message);
        Task<Message> RemoveMessageAsync(Guid messageId);
        Task<List<Message>> GetMessageToDelivery();
        Task UpdateMessages(List<Message> messages);
        Task UpdateMessage(Message message);
    }
}
