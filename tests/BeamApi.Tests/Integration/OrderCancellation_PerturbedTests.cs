using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class OrderCancellationPerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrderCancellationPerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CancelOrder_ShouldUpdateStatusAndReverseAllRelatedEffects()
    {
        var payload = new
        {
            orderId = "order-301"
        };

        var response = await _client.PostAsJsonAsync("/api/orders/cancel", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Cancelled");
        body.Should().Contain("\"inventoryReleased\":true");
        body.Should().Contain("\"billingHoldRemoved\":true");
    }

    [Fact]
    public async Task CancelOrder_MissingOrderId_ShouldStillFail_With422()
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