using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class TicketStateT22OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TicketStateT22OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Transition_OpenResolve_ReturnsResolved()
    {
        var payload = new
        {
            ticketId = $"ticket-{Guid.NewGuid():N}",
            currentStatus = "Open",
            action = "resolve",
            hoursSinceClosed = 0
        };

        var response =
            await _client.PostAsJsonAsync("/api/tickets/state/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Resolved");
    }

    [Fact]
    public async Task Transition_ResolvedClose_ReturnsClosed()
    {
        var payload = new
        {
            ticketId = $"ticket-{Guid.NewGuid():N}",
            currentStatus = "Resolved",
            action = "close",
            hoursSinceClosed = 0
        };

        var response =
            await _client.PostAsJsonAsync("/api/tickets/state/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Closed");
    }

    [Fact]
    public async Task Transition_UnsupportedAction_LeavesStatusUnchanged()
    {
        var payload = new
        {
            ticketId = $"ticket-{Guid.NewGuid():N}",
            currentStatus = "Closed",
            action = "reopen",
            hoursSinceClosed = 24
        };

        var response =
            await _client.PostAsJsonAsync("/api/tickets/state/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Closed");
        body.Should().NotContain("\"status\":\"Open\"");
    }
}