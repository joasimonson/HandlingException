using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FluentAssertions;

namespace HandlingExceptionsDomain.Tests;

public class ResultExtensionsTests
{
    [Fact]
    public void Result_IsSuccess_Returns_SuccessDetails()
    {
        // Arrange
        var successResult = Result.Success();

        // Act
        var result = successResult.Result();

        // Assert
        result.Should().BeOfType<Ok<SuccessDetails>>()
            .Which.Value.Should().BeEquivalentTo(new SuccessDetails
            {
                Status = StatusCodes.Status200OK,
                Title = "Success"
            });
    }

    [Fact]
    public void Result_IsFailure_Returns_ProblemDetails()
    {
        // Arrange
        var error = new Error("123", "Some error");
        var failureResult = Result.Failure(error);

        // Act
        var result = failureResult.Result();

        // Assert
        result.Should().BeOfType<ProblemHttpResult>()
            .Which.ProblemDetails.Should().BeEquivalentTo(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Extensions = new Dictionary<string, object?>
                {
                    { "errors", new[] { error } }
                }
            });
    }

    [Fact]
    public void ToSuccess_WithFailureResult_ThrowsInvalidOperationException()
    {
        // Arrange
        var failureResult = Result.Failure(new Error("123", "Some error"));

        // Act
        Action act = () => failureResult.ToSuccess();

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Can't convert problem result to success");
    }

    [Fact]
    public void ToProblemDetails_WithSuccessResult_ThrowsInvalidOperationException()
    {
        // Arrange
        var successResult = Result.Success();

        // Act
        Action act = () => successResult.ToProblemDetails();

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Can't convert success result to problem");
    }
}