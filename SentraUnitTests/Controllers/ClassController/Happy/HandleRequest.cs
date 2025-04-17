using HelloBear.Api.Attributes;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

[Collection("ClassControllerTests")]
public class ClassControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ClassController _classController;

    public ClassControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _classController = new ClassController(_mediatorMock.Object);
    }

    [Fact]
    public async Task HandleRequest_ReturnsOkObjectResult_WhenRequestIsSuccessful()
    {
        // Arrange
        var operationResult = new OperationResult { Success = true };
        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(operationResult);

        // Act
        var result = await _classController.HandleRequest(new CreateClassCommand());

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.Equal(operationResult, okResult.Value);
    }

    [Fact]
    public async Task HandleRequest_ReturnsBadRequestObjectResult_WhenRequestFails()
    {
        // Arrange
        var operationResult = new OperationResult { Success = false, Errors = new List<string> { "Error" } };
        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(operationResult);

        // Act
        var result = await _classController.HandleRequest(new CreateClassCommand());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.Equal(operationResult.Errors, badRequestResult.Value);
    }

    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenRequestIsNull()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync((OperationResult)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _classController.HandleRequest(null));
    }
}