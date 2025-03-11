using System.ComponentModel.DataAnnotations.Schema;

namespace MessageToFuture.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }  
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
