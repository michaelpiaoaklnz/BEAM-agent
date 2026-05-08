using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BeamApi.Tests.Integration;

public class OrdersQuantityT14OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrdersQuantityT14OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("electronics", 1)]
    [InlineData("electronics", 6)]
    [InlineData("office", 1000)]
    [InlineData("bulk", 1)]
    public async Task Submit_WithAnyPositiveQuantity_ReturnsSuccess(string category, int quantity)
    {
        var payload = new OrderSubmitRequest
        {
            CustomerName = "Test Customer",
            ShippingAddress = "1 Test Street",
            ContainsFragileItems = false,
            ProductId = $"product-{Guid.NewGuid():N}",
            Category = category,
            Quantity = quantity,
            DeliveryInstructions = null
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
        body.Should().Contain("Order submitted successfully");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task Submit_WithZeroOrNegativeQuantity_ReturnsValidationFailure(int quantity)
    {
        var payload = new OrderSubmitRequest
        {
            CustomerName = "Test Customer",
            ShippingAddress = "1 Test Street",
            ContainsFragileItems = false,
            ProductId = $"product-{Guid.NewGuid():N}",
            Category = "electronics",
            Quantity = quantity,
            DeliveryInstructions = null
        };

        var response = await _client.PostAsJsonAsync("/api/orders/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation failed");
        body.Should().Contain("false");
        body.Should().Contain("Quantity must be greater than zero.");
    }
}
