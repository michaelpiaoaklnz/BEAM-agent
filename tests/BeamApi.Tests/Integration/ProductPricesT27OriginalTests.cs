using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ProductPricesT27OriginalTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductPricesT27OriginalTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task UpdatePrice_OriginalBehavior_UpdatesProductRecordOnly()
    {
        var payload = new
        {
            productId = $"product-{Guid.NewGuid():N}",
            newPrice = 199.99m
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/product-prices/update",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"productUpdated\":true");
        body.Should().Contain("\"priceHistoryCreated\":false");
        body.Should().Contain("\"cacheInvalidated\":false");
    }
}   