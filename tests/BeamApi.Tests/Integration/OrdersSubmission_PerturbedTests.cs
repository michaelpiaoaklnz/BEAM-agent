using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class OrdersSubmissionPerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrdersSubmissionPerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Submit_FragileOrder_WithoutDeliveryInstructions_ShouldFail_With422()
    {
        var payload = new
        {
            customerName = "Bob",
            shippingAddress = "456 King Street",
            containsFragileItems = true
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation");
        body.Should().Contain("delivery", "the error should mention delivery instructions");
    }

    [Fact]
    public async Task Submit_FragileOrder_WithDeliveryInstructions_ShouldSucceed()
    {
        var payload = new
        {
            customerName = "Carol",
            shippingAddress = "789 High Street",
            containsFragileItems = true,
            deliveryInstructions = "Handle with care and call on arrival"
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Submit_NonFragileOrder_WithoutDeliveryInstructions_ShouldStillSucceed()
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
}