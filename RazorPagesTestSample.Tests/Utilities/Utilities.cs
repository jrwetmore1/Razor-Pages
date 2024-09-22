using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesTestSample.Data; // Ensure this matches your main project's namespace

namespace RazorPagesTestSample.Tests.Utilities
{
    public static class Utilities
    {
        public static DbContextOptions<AppDbContext> TestDbContextOptions()
        {
            // Create a new service provider for the in-memory database
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create new DbContextOptions using the in-memory database
            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
