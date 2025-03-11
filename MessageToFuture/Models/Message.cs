using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MessageToFuture.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public bool IsDelivered { get; set; } = false;

        [JsonIgnore]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
