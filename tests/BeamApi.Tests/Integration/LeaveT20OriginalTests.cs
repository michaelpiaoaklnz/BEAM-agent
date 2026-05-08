using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class LeaveT20OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public LeaveT20OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(0.5, true)]
    [InlineData(1.5, true)]
    [InlineData(2.0, false)]
    [InlineData(3.0, false)]
    public async Task Submit_OriginalDurationThreshold_WorksCorrectly(
        decimal daysRequested,
        bool expectedAutoApproved)
    {
        var payload = new
        {
            daysRequested,
            remainingLeaveBalance = 0,
            teamMembersAvailable = 0
        };

        var response =
            await _client.PostAsJsonAsync("/api/leave/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain(
            $"\"autoApproved\":{expectedAutoApproved.ToString().ToLower()}");
    }
}