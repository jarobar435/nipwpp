using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Posts.Api.Controllers;
using Posts.Api.Models;
using Posts.Api.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Posts.UnitTests
{
    public class BlogPostsV2ControllerTest
    {
        [Fact]
        public async Task ShouldReturnEmptyPageWhenCallingGetWithParamsOnEmptyRepo()
        {
            // Given
            var mockLogger = new Mock<ILogger<BlogPostsV2Controller>>();
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.GetAllPagedAsync(It.IsAny<int>(), It.IsAny<int>(), null))
            .ReturnsAsync(new PaginatedItems<BlogPost>());
            var controller = new BlogPostsV2Controller(mockLogger.Object, mockRepo.Object);
            // When
            var result = await controller.Get(0, 5);
            // Then
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PaginatedItems<BlogPost>>(actionResult.Value);
            Assert.Null(returnValue.Items);
            Assert.Equal(0, returnValue.PageIndex);
            Assert.Equal(0, returnValue.PageSize);
            Assert.Equal(0, returnValue.TotalItems);
            Assert.Null(returnValue.NextPage);
        }
    }
}
