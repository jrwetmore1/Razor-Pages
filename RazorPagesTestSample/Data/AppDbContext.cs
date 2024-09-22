using Microsoft.EntityFrameworkCore;

namespace RazorPagesTestSample.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await Messages.OrderBy(m => m.Text).ToListAsync();
        }

        public async Task AddMessageAsync(Message message)
        {
            Messages.Add(message);
            await SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(int id)
        {
            var message = await Messages.FindAsync(id);
            if (message != null)
            {
                Messages.Remove(message);
                await SaveChangesAsync();
            }
        }

        public async Task DeleteAllMessagesAsync()
        {
            Messages.RemoveRange(Messages);
            await SaveChangesAsync();
        }
    }
}
