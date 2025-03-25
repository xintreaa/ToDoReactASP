using Microsoft.EntityFrameworkCore;

namespace ToDoWebAPI
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<Quest> Quests { get; set; } // Це властивість, яка представляє таблицю в базі даних

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options)
        {
        }
    }
}