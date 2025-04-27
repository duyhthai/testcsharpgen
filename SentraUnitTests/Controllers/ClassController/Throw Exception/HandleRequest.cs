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
    public async void HandleRequest_ThrowsInvalidOperationException_WhenRequestIsNull()
    {
        // Arrange
        var controller = new ClassController();
        var mockMediator = new Mock<IMediator>();
        controller.Mediator = mockMediator.Object;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(null));
    }

    [Fact]
    public async void HandleRequest_ThrowsInvalidOperationException_WhenMediatorSendThrowsException()
    {
        // Arrange
        var controller = new ClassController();
        var mockMediator = new Mock<IMediator>();
        controller.Mediator = mockMediator.Object;

        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).Throws(new Exception("Test exception"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => controller.HandleRequest(It.IsAny<IRequest<OperationResult>>()));
    }
}