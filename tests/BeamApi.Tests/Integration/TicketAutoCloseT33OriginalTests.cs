using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class TicketAutoCloseT33OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TicketAutoCloseT33OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("low", 7, true)]
    [InlineData("low", 10, true)]
    [InlineData("low", 6, false)]
    [InlineData("high", 10, false)]
    public async Task Evaluate_OriginalAutoCloseRule_WorksCorrectly(
        string priority,
        int inactiveDays,
        bool expectedAutoClosed)
    {
        var payload = new
        {
            ticketId = $"ticket-{Guid.NewGuid():N}",
            priority,
            inactiveDays,
            hasUnresolvedCustomerReply = true,
            hasPendingEscalation = true
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/tickets/autoclose/evaluate",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain(
            $"\"autoClosed\":{expectedAutoClosed.ToString().ToLower()}");
    }
}