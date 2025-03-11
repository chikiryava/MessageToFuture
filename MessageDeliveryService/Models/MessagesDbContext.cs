using Microsoft.EntityFrameworkCore;

namespace MessageToFuture.Models
{
    public class MessagesDbContext: DbContext
    {
        public MessagesDbContext(DbContextOptions<MessagesDbContext> contextOptions): base(contextOptions)
        {

        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
