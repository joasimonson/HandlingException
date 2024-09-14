using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HandlingExceptionsDomain.Tests;

public class DomainRequestApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public DomainRequestApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_DomainRequest_WithFutureDate_ReturnsBadRequest()
    {
        // Arrange
        var futureDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

        // Act
        var response = await _client.GetAsync($"/domain-request/{futureDate}/true");

        var a = await response.Content.ReadAsStringAsync();

        // Assert
        response.Should().Be400BadRequest().And.BeAs(new
        {
            errors = new[]
            {
                new
                {
                    code = ServiceErrors.GreaterThanToday.Code,
                    description = ServiceErrors.GreaterThanToday.Description
                }
            }
        });
    }

    [Fact]
    public async Task Get_DomainRequest_WithInvalidOperation_ReturnsBadRequest()
    {
        // Arrange
        var valid = false;
        var todayDate = DateTime.Now.ToString("yyyy-MM-dd");

        // Act
        var response = await _client.GetAsync($"/domain-request/{todayDate}/{valid}");

        // Assert
        response.Should().Be400BadRequest().And.BeAs(new
        {
            errors = new[]
            {
                new
                {
                    code = ServiceErrors.InvalidOperation.Code,
                    description = ServiceErrors.InvalidOperation.Description
                }
            }
        });
    }

    [Fact]
    public async Task Get_DomainRequest_WithValidOperation_ReturnsOk()
    {
        // Arrange
        var valid = true;
        var todayDate = DateTime.Now.ToString("yyyy-MM-dd");

        // Act
        var response = await _client.GetAsync($"/domain-request/{todayDate}/{valid}");

        // Assert
        response.Should().Be200Ok();
    }

    [Fact]
    public async Task Get_DomainRequest_WhenCancelled_ThrowsTaskCancelledException()
    {
        // Arrange
        var valid = true;
        var todayDate = DateTime.Now.ToString("yyyy-MM-dd");
        var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act
        Func<Task> act = async () => await _client.GetAsync($"/domain-request/{todayDate}/{valid}", cts.Token);

        // Assert
        await act.Should().ThrowAsync<TaskCanceledException>();
    }
}