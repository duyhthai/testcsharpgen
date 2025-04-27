using HelloBear.Api.Controllers;
using HelloBear.Application.Classes.Commands.CreateClass;
using HelloBear.Application.Classes.Commands.GenerateQrCode;
using HelloBear.Application.Classes.Queries.GetClassDetail;
using HelloBear.Application.Classes.Queries.GetClassesWithPagination;
using HelloBear.Application.Classes.Shared.Models;
using HelloBear.Application.Common.Models.Paging;
using HelloBear.Application.Lessons.Queries.GetLessonsByClass;
using HelloBear.Application.Settings;
using HelloBear.Application.Textbooks.Queries.GetAllTextBooks;
using HelloBear.Application.TextBooks.Queries.GetTextbooksWithPagination;
using Moq;
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
            _controller._mediator = _mediatorMock.Object;
        }

        [Fact]
        public async Task GetClassesWithPagination_ReturnsPaginatedListOfClassResponse()
        {
            // Arrange
            var query = new GetClassesWithPaginationQuery { PageNumber = 1, PageSize = 10 };
            var expectedResult = new PaginatedList<ClassResponse>
            {
                Items = new List<ClassResponse>(),
                TotalCount = 0,
                PageIndex = 1,
                PageSize = 10
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetClassesWithPaginationQuery>())).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetClassesWithPagination(query);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<PaginatedList<ClassResponse>>(okObjectResult.Value);
            Assert.Equal(expectedResult.Items, actualResult.Items);
            Assert.Equal(expectedResult.TotalCount, actualResult.TotalCount);
            Assert.Equal(expectedResult.PageIndex, actualResult.PageIndex);
            Assert.Equal(expectedResult.PageSize, actualResult.PageSize);
        }

        [Fact]
        public async Task GetClassById_ReturnsClassDetailResponse()
        {
            // Arrange
            int id = 1;
            var expectedResult = new ClassDetailResponse();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetClassDetailQuery>())).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetClassById(id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<ClassDetailResponse>(okObjectResult.Value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult()
        {
            // Arrange
            var createClassCommand = new CreateClassCommand { Name = "Test Class" };
            int expectedResult = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateClassCommand>())).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Create(createClassCommand);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(expectedResult, createdAtActionResult.Value);
        }
    }
}