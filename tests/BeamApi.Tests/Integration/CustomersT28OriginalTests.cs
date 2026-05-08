using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class CustomersT28OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CustomersT28OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Delete_OriginalBehavior_DeletesProfileOnly()
    {
        var payload = new
        {
            customerId = $"customer-{Guid.NewGuid():N}",
            hasRelatedOrders = true,
            hasActiveDisputes = true
        };

        var response = await _client.PostAsJsonAsync("/api/customers/delete", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"profileDeleted\":true");
        body.Should().Contain("\"relatedOrdersArchived\":false");
        body.Should().Contain("\"futureInvoicesBlocked\":false");
        body.Should().Contain("\"deletionBlocked\":false");
    }
}