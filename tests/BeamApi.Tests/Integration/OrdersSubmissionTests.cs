using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class OrdersSubmissionTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrdersSubmissionTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Submit_NonFragileOrder_WithoutDeliveryInstructions_ShouldSucceed()
    {
        var payload = new
        {
            customerName = "Alice",
            shippingAddress = "123 Queen Street",
            containsFragileItems = false
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Submit_FragileOrder_WithoutDeliveryInstructions_ShouldSucceed()
    {
        var payload = new
        {
            customerName = "Bob",
            shippingAddress = "456 King Street",
            containsFragileItems = true
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Submit_Order_WithDeliveryInstructions_ShouldSucceed()
    {
        var payload = new
        {
            customerName = "Carol",
            shippingAddress = "789 High Street",
            containsFragileItems = true,
            deliveryInstructions = "Leave at front desk"
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }
}