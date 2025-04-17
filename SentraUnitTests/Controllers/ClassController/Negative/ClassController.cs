using HelloBear.Api.Controllers;
using HelloBear.Application.Classes.Commands.CreateClass;
using HelloBear.Application.Classes.Queries.GetClassDetail;
using HelloBear.Application.Classes.Queries.GetClassesWithPagination;
using HelloBear.Application.Common.Models.Paging;
using HelloBear.Application.Lessons.Queries.GetLessonsByClass;
using HelloBear.Application.Settings;
using HelloBear.Application.Textbooks.Queries.GetAllTextBooks;
using HelloBear.Application.TextBooks.Queries.GetTextbooksWithPagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HelloBear.Api.Tests.Controllers
{
    public class ClassControllerTests
    {
        private readonly Mock<ISender> _mediatorMock;
        private readonly ClassController _controller;

        public ClassControllerTests()
        {
            _mediatorMock = new Mock<ISender>();
            _controller = new ClassController();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Fact]
        public async Task GetClassesWithPagination_InvalidQuery_ReturnsBadRequest()
        {
            // Arrange
            var query = new GetClassesWithPaginationQuery { PageNumber = 0, PageSize = 10 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetClassesWithPaginationQuery>()))
                          .ReturnsAsync((OperationResult<PaginatedList<ClassResponse>>)null);

            // Act
            var result = await _controller.GetClassesWithPagination(query);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetClassById_NonExistentId_ReturnsNotFound()
        {
            // Arrange
            int id = 999;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetClassDetailQuery>()))
                          .ReturnsAsync((OperationResult<ClassDetailResponse>)null);

            // Act
            var result = await _controller.GetClassById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Create_InvalidCommand_ReturnsBadRequest()
        {
            // Arrange
            var command = new CreateClassCommand { Name = "" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateClassCommand>()))
                          .ReturnsAsync((OperationResult<int>)null);

            // Act
            var result = await _controller.Create(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}