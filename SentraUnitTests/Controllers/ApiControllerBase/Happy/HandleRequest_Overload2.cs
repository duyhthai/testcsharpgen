using System;
using System.Threading.Tasks;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ApiControllerBaseTests
{
    [Fact]
    public async Task HandleRequest_ReturnsOkObjectResult_WhenOperationResultIsSuccess()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var expectedResult = new OperationResult { Success = true };
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(expectedResult);

        var controller = new TestApiControllerBase(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(new Mock<IRequest<OperationResult>>().Object);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedResult, okObjectResult.Value);
    }

    [Fact]
    public async Task HandleRequest_ReturnsBadRequestObjectResult_WhenOperationResultIsFailure()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var expectedResult = new OperationResult { Success = false, Errors = new[] { "Error" } };
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(expectedResult);

        var controller = new TestApiControllerBase(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(new Mock<IRequest<OperationResult>>().Object);

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(expectedResult.Errors, badRequestObjectResult.Value);
    }

    private class TestApiControllerBase : ApiControllerBase
    {
        public TestApiControllerBase(IMediator mediator) : base(mediator)
        {
        }
    }
}