using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ApiControllerBaseTests
{
    [Fact]
    public async Task HandleRequest_ReturnsBadRequestObjectResult_WhenOperationResultHasErrors()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var operationResultWithErrors = new OperationResult
        {
            Success = false,
            Errors = new List<string> { "Error 1", "Error 2" }
        };
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(operationResultWithErrors);

        var apiControllerBase = new ApiControllerBase(mockMediator.Object);

        // Act
        var result = await apiControllerBase.HandleRequest(new Mock<IRequest<OperationResult>>().Object);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.NotNull(badRequestResult.Value);
        Assert.IsAssignableFrom<IEnumerable<string>>(badRequestResult.Value);
        var errors = badRequestResult.Value as IEnumerable<string>;
        Assert.Equal(2, errors.Count());
        Assert.Contains("Error 1", errors);
        Assert.Contains("Error 2", errors);
    }

    [Fact]
    public async Task HandleRequest_ReturnsUnprocessableEntityObjectResult_WhenOperationResultHasValidationErrors()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var validationContext = new ValidationContext(new object());
        var validationResults = new List<ValidationResult>
        {
            new ValidationResult("Validation Error 1"),
            new ValidationResult("Validation Error 2")
        };

        var operationResultWithValidationErrors = new OperationResult
        {
            Success = false,
            ValidationResults = validationResults
        };
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(operationResultWithValidationErrors);

        var apiControllerBase = new ApiControllerBase(mockMediator.Object);

        // Act
        var result = await apiControllerBase.HandleRequest(new Mock<IRequest<OperationResult>>().Object);

        // Assert
        Assert.IsType<UnprocessableEntityObjectResult>(result);
        var unprocessableEntityResult = result as UnprocessableEntityObjectResult;
        Assert.NotNull(unprocessableEntityResult.Value);
        Assert.IsAssignableFrom<IEnumerable<ValidationResult>>(unprocessableEntityResult.Value);
        var validationResultsFromResponse = unprocessableEntityResult.Value as IEnumerable<ValidationResult>;
        Assert.Equal(2, validationResultsFromResponse.Count());
        Assert.Contains(validationResults[0], validationResultsFromResponse);
        Assert.Contains(validationResults[1], validationResultsFromResponse);
    }

    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenOperationResultIsNull()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync((OperationResult)null);

        var apiControllerBase = new ApiControllerBase(mockMediator.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiControllerBase.HandleRequest(new Mock<IRequest<OperationResult>>().Object));
    }
}