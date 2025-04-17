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
        public async Task GetClassesWithPagination_MediatorReturnsNull_ThrowsInvalidOperationException()
        {
            // Arrange
            var query = new GetClassesWithPaginationQuery();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetClassesWithPaginationQuery>())).ReturnsAsync((PaginatedList<ClassResponse>)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.GetClassesWithPagination(query));
        }

        [Fact]
        public async Task GetClassById_MediatorReturnsNull_ThrowsInvalidOperationException()
        {
            // Arrange
            int id = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetClassDetailQuery>())).ReturnsAsync((ClassDetailResponse)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.GetClassById(id));
        }

        [Fact]
        public async Task Create_MediatorReturnsNull_ThrowsInvalidOperationException()
        {
            // Arrange
            var createClassCommand = new CreateClassCommand();

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateClassCommand>())).ReturnsAsync((int?)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Create(createClassCommand));
        }
    }
}