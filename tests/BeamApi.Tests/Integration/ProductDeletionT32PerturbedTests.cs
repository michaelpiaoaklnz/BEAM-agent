using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ProductDeletionT32PerturbedTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductDeletionT32PerturbedTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Delete_PerturbedBehavior_PropagatesAllEffects()
    {
        var payload = new
        {
            productId = $"product-{Guid.NewGuid():N}",
            hasExistingOrders = true
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/product-deletion/delete",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"removedFromCatalogue\":true");
        body.Should().Contain("\"newOrdersBlocked\":true");
        body.Should().Contain("\"orderHistoryPreserved\":true");
    }
}