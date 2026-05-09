using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class TicketAutoCloseT33PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TicketAutoCloseT33PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Evaluate_LowPriorityInactiveWithoutExceptions_AutoCloses()
    {
        var payload = new
        {
            ticketId = $"ticket-{Guid.NewGuid():N}",
            priority = "low",
            inactiveDays = 8,
            hasUnresolvedCustomerReply = false,
            hasPendingEscalation = false
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/tickets/autoclose/evaluate",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"autoClosed\":true");
    }

    [Fact]
    public async Task Evaluate_WithUnresolvedCustomerReply_DoesNotAutoClose()
    {
        var payload = new
        {
            ticketId = $"ticket-{Guid.NewGuid():N}",
            priority = "low",
            inactiveDays = 8,
            hasUnresolvedCustomerReply = true,
            hasPendingEscalation = false
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/tickets/autoclose/evaluate",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"autoClosed\":false");
    }

    [Fact]
    public async Task Evaluate_WithPendingEscalation_DoesNotAutoClose()
    {
        var payload = new
        {
            ticketId = $"ticket-{Guid.NewGuid():N}",
            priority = "low",
            inactiveDays = 8,
            hasUnresolvedCustomerReply = false,
            hasPendingEscalation = true
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/tickets/autoclose/evaluate",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"autoClosed\":false");
    }
}
