using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class CustomersT28PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CustomersT28PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Delete_WithRelatedOrders_ArchivesOrdersAndBlocksFutureInvoices()
    {
        var payload = new
        {
            customerId = $"customer-{Guid.NewGuid():N}",
            hasRelatedOrders = true,
            hasActiveDisputes = false
        };

        var response = await _client.PostAsJsonAsync("/api/customers/delete", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"profileDeleted\":true");
        body.Should().Contain("\"relatedOrdersArchived\":true");
        body.Should().Contain("\"futureInvoicesBlocked\":true");
        body.Should().Contain("\"deletionBlocked\":false");
    }

    [Fact]
    public async Task Delete_WithActiveDisputes_BlocksDeletion()
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
        body.Should().Contain("\"profileDeleted\":false");
        body.Should().Contain("\"deletionBlocked\":true");
    }
}