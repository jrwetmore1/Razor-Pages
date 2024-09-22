using Microsoft.AspNetCore.Mvc;        
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;                             
using RazorPagesTestSample.Data;       
using RazorPagesTestSample.Pages;      
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using RazorPagesTestSample.Tests.Utilities;                       


namespace RazorPagesTestSample.Tests.UnitTests
{
    public class IndexPageTests
    {
        [Fact]
        public async Task OnGetAsync_PopulatesThePageModel_WithAListOfMessages()
        {
            // Arrange
            var expectedMessages = new List<Message>
            {
                new Message { Id = 1, Text = "Message 1" },
                new Message { Id = 2, Text = "Message 2" }
            };

            var mockAppDbContext = new Mock<AppDbContext>();
            mockAppDbContext.Setup(db => db.GetMessagesAsync())
                .ReturnsAsync(expectedMessages);

            var pageModel = new IndexModel(mockAppDbContext.Object);

            // Act
            await pageModel.OnGetAsync();

            // Assert
            var actualMessages = Assert.IsAssignableFrom<List<Message>>(pageModel.Messages);
            Assert.Equal(expectedMessages.Count, actualMessages.Count);
        }
    }
}
