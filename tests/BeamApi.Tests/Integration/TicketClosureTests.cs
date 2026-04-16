using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class TicketClosureTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TicketClosureTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CloseTicket_After7Days_ShouldAutoClose()
    {
        var payload = new
        {
            ticketId = "ticket-1201",
            daysOpen = 7,
            isPriority = false,
            manualClosureRequested = false
        };

        var response = await _client.PostAsJsonAsync("/api/tickets/close", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"status\":\"Closed\"");
        body.Should().Contain("\"closureMode\":\"Auto\"");
    }

    [Fact]
    public async Task CloseTicket_Before7Days_ShouldFail_With422()
    {
        var payload = new
        {
            ticketId = "ticket-1202",
            daysOpen = 3,
            isPriority = false,
            manualClosureRequested = false
        };

        var response = await _client.PostAsJsonAsync("/api/tickets/close", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("failed");
    }
}
