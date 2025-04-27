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
public class ClassControllerNegativeTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ClassController _classController;

    public ClassControllerNegativeTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _classController = new ClassController(_mediatorMock.Object);
    }

    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenRequestIsNull()
    {
        // Arrange
        IRequest<OperationResult> nullRequest = null;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _classController.HandleRequest(nullRequest));
    }

    [Fact]
    public async Task HandleRequest_ReturnsInternalServerError_WhenMediatorSendThrowsException()
    {
        // Arrange
        var exception = new Exception("Test exception");
        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).Throws(exception);

        // Act
        var result = await _classController.HandleRequest(It.IsAny<IRequest<OperationResult>>());

        // Assert
        Assert.IsType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
    }
}