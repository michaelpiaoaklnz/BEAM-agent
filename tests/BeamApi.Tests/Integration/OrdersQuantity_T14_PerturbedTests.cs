using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace BeamApi.Tests.Integration;

public class OrdersQuantityT14PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrdersQuantityT14PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("electronics", 1)]
    [InlineData("electronics", 5)]
    [InlineData("office", 1)]
    [InlineData("office", 100)]
    [InlineData("bulk", 10)]
    [InlineData("bulk", 1000)]
    public async Task Submit_WithQuantityWithinCategoryLimits_ReturnsSuccess(string category, int quantity)
    {
        var payload = new
        {
            customerName = "Test Customer",
            shippingAddress = "1 Test Street",
            containsFragileItems = false,
            productId = $"product-{Guid.NewGuid():N}",
            category,
            quantity,
            deliveryInstructions = (string?)null
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
        body.Should().Contain("Order submitted successfully");
    }

    [Theory]
    [InlineData("electronics", 6)]
    [InlineData("office", 101)]
    [InlineData("bulk", 9)]
    [InlineData("bulk", 1001)]
    public async Task Submit_WithQuantityOutsideCategoryLimits_ReturnsFailure(string category, int quantity)
    {
        var payload = new
        {
            customerName = "Test Customer",
            shippingAddress = "1 Test Street",
            containsFragileItems = false,
            productId = $"product-{Guid.NewGuid():N}",
            category,
            quantity,
            deliveryInstructions = (string?)null
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("false");
        body.Should().Contain("quantity");
    }

    [Fact]
    public async Task Submit_WithUnknownCategory_ReturnsFailure()
    {
        var payload = new
        {
            customerName = "Test Customer",
            shippingAddress = "1 Test Street",
            containsFragileItems = false,
            productId = $"product-{Guid.NewGuid():N}",
            category = "unknown",
            quantity = 5,
            deliveryInstructions = (string?)null
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("false");
        body.Should().Contain("category");
    }
}
