using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class TicketClosurePerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TicketClosurePerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CloseTicket_NonPriority_After7Days_ShouldStillAutoClose()
    {
        var payload = new
        {
            ticketId = "ticket-1301",
            daysOpen = 8,
            isPriority = false,
            manualClosureRequested = false
        };

        var response = await _client.PostAsJsonAsync("/api/tickets/close", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"closureMode\":\"Auto\"");
    }

    [Fact]
    public async Task CloseTicket_Priority_After7Days_ShouldNotAutoClose()
    {
        var payload = new
        {
            ticketId = "ticket-1302",
            daysOpen = 8,
            isPriority = true,
            manualClosureRequested = false
        };

        var response = await _client.PostAsJsonAsync("/api/tickets/close", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("manual");
    }

    [Fact]
    public async Task CloseTicket_Priority_ManualClosure_ShouldSucceed()
    {
        var payload = new
        {
            ticketId = "ticket-1303",
            daysOpen = 8,
            isPriority = true,
            manualClosureRequested = true
        };

        var response = await _client.PostAsJsonAsync("/api/tickets/close", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"status\":\"Closed\"");
        body.Should().Contain("\"closureMode\":\"Manual\"");
    }
}