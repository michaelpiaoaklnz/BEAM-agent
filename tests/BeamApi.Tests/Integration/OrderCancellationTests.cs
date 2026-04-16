using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class OrderCancellationTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrderCancellationTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CancelOrder_ShouldUpdateStatusToCancelled()
    {
        var payload = new
        {
            orderId = "order-201"
        };

        var response = await _client.PostAsJsonAsync("/api/orders/cancel", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Cancelled");
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task CancelOrder_MissingOrderId_ShouldFail_With422()
    {
        var payload = new
        {
            orderId = ""
        };

        var response = await _client.PostAsJsonAsync("/api/orders/cancel", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation");
    }
}