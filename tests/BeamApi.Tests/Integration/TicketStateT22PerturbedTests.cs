using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class TicketStateT22PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TicketStateT22PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(24)]
    [InlineData(48)]
    public async Task Transition_ClosedReopenWithin48Hours_ReturnsOpen(int hoursSinceClosed)
    {
        var payload = new
        {
            ticketId = $"ticket-{Guid.NewGuid():N}",
            currentStatus = "Closed",
            action = "reopen",
            hoursSinceClosed
        };

        var response =
            await _client.PostAsJsonAsync("/api/tickets/state/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"status\":\"Open\"");
    }

    [Theory]
    [InlineData(49)]
    [InlineData(72)]
    public async Task Transition_ClosedReopenAfter48Hours_RemainsClosed(int hoursSinceClosed)
    {
        var payload = new
        {
            ticketId = $"ticket-{Guid.NewGuid():N}",
            currentStatus = "Closed",
            action = "reopen",
            hoursSinceClosed
        };

        var response =
            await _client.PostAsJsonAsync("/api/tickets/state/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"status\":\"Closed\"");
        body.Should().NotContain("\"status\":\"Open\"");
    }

    [Fact]
    public async Task Transition_OpenResolve_StillReturnsResolved()
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
        body.Should().Contain("\"status\":\"Resolved\"");
    }

    [Fact]
    public async Task Transition_ResolvedClose_StillReturnsClosed()
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
        body.Should().Contain("\"status\":\"Closed\"");
    }
}
