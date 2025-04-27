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
using HelloBear.Api.Controllers;
using HelloBear.Application.Interfaces;
using Moq;
using Xunit;

public class ClassControllerTests
{
    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenRequestIsNull()
    {
        // Arrange
        var controller = new ClassController();
        controller.Mediator = Mock.Of<IMediator>();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(null));
    }

    [Fact]
    public async Task HandleRequest_ReturnsInternalServerError_WhenMediatorSendThrowsException()
    {
        // Arrange
        var controller = new ClassController();
        controller.Mediator = Mock.Of<IMediator>();
        Mock.Get(controller.Mediator).Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).Throws(new Exception());

        // Act & Assert
        var result = await controller.HandleRequest(Mock.Of<IRequest<OperationResult>>());
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, ((ObjectResult)result).StatusCode);
    }

    [Fact]
    public async Task HandleRequest_ReturnsOkObjectResult_WhenRequestIsSuccessful()
    {
        // Arrange
        var controller = new ClassController();
        controller.Mediator = Mock.Of<IMediator>();
        var expectedResult = new OperationResult { Success = true };
        Mock.Get(controller.Mediator).Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(expectedResult);

        // Act
        var result = await controller.HandleRequest(Mock.Of<IRequest<OperationResult>>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedResult, ((OkObjectResult)result).Value);
    }
}