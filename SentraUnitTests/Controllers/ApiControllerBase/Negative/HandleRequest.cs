using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

[TestFixture]
public class ApiControllerBaseTests
{
    [Test]
    public void HandleRequest_ThrowsInvalidOperationException_WhenOperationResultIsNull()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync((OperationResult?)null);

        var controller = new TestApiControllerBase(mockMediator.Object);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(new Mock<IRequest<OperationResult>>().Object));
    }

    [Test]
    public void HandleRequest_ReturnsBadRequestObjectResult_WhenOperationResultHasErrors()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(new OperationResult { Success = false, Errors = new List<string> { "Error 1", "Error 2" } });

        var controller = new TestApiControllerBase(mockMediator.Object);

        // Act
        var result = controller.HandleRequest(new Mock<IRequest<OperationResult>>().Object);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Error 1\nError 2", badRequestResult.Value.ToString());
    }

    [Test]
    public void HandleRequest_ReturnsUnprocessableEntityObjectResult_WhenOperationResultHasValidationErrors()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(new OperationResult { Success = false, ValidationErrors = new List<ValidationResult> { new ValidationResult("Validation Error") } });

        var controller = new TestApiControllerBase(mockMediator.Object);

        // Act
        var result = controller.HandleRequest(new Mock<IRequest<OperationResult>>().Object);

        // Assert
        Assert.IsInstanceOf<UnprocessableEntityObjectResult>(result);
        var unprocessableEntityResult = result as UnprocessableEntityObjectResult;
        Assert.IsNotNull(unprocessableEntityResult);
        Assert.AreEqual("Validation Error", unprocessableEntityResult.Value.ToString());
    }
}

public class TestApiControllerBase : ApiControllerBase
{
    public TestApiControllerBase(IMediator mediator) : base(mediator)
    {
    }
}