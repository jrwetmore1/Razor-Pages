using Microsoft.EntityFrameworkCore; 
using RazorPagesTestSample.Data;     
using RazorPagesTestSample.Tests.Utilities;
using System.Collections.Generic;    
using System.Threading.Tasks;        
using Xunit;
using RazorPagesTestSample.Tests.Utilities;
                 

namespace RazorPagesTestSample.Tests.UnitTests
{
    public class DataAccessLayerTest
    {
        [Fact]
        public async Task GetMessagesAsync_ReturnsAllMessagesOrderedByText()
        {
            // Use the TestDbContextOptions to get a fresh in-memory database context
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                // Arrange: Seed the in-memory database with sample data
                var seedMessages = new List<Message>
                {
                    new Message { Id = 1, Text = "Zebra" },
                    new Message { Id = 2, Text = "Apple" },
                    new Message { Id = 3, Text = "Banana" }
                };
                await db.AddRangeAsync(seedMessages);
                await db.SaveChangesAsync();

                // Act: Fetch the messages from the database
                var messages = await db.GetMessagesAsync();

                // Assert: Ensure that messages are ordered by Text property
                Assert.Equal(3, messages.Count);
                Assert.Equal("Apple", messages[0].Text);
                Assert.Equal("Banana", messages[1].Text);
                Assert.Equal("Zebra", messages[2].Text);
            }
        }

        [Fact]
        public async Task AddMessageAsync_AddsMessageToDatabase()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var newMessage = new Message { Text = "New message" };

                // Act
                await db.AddMessageAsync(newMessage);
                var messages = await db.Messages.ToListAsync();

                // Assert
                Assert.Single(messages);
                Assert.Equal("New message", messages[0].Text);
            }
        }
    }
}
