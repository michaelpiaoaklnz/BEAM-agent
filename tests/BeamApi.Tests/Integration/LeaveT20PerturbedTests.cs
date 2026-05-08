using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class LeaveT20PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public LeaveT20PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ShortLeave_WithSufficientBalanceAndStaffing_ShouldAutoApprove()
    {
        var payload = new
        {
            daysRequested = 1.5,
            remainingLeaveBalance = 5,
            teamMembersAvailable = 3
        };

        var response =
            await _client.PostAsJsonAsync("/api/leave/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"autoApproved\":true");
    }

    [Fact]
    public async Task ShortLeave_WithInsufficientBalance_ShouldRequireManualReview()
    {
        var payload = new
        {
            daysRequested = 1.5,
            remainingLeaveBalance = 1,
            teamMembersAvailable = 3
        };

        var response =
            await _client.PostAsJsonAsync("/api/leave/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"autoApproved\":false");
    }

    [Fact]
    public async Task ShortLeave_WithInsufficientStaffing_ShouldRequireManualReview()
    {
        var payload = new
        {
            daysRequested = 1.5,
            remainingLeaveBalance = 5,
            teamMembersAvailable = 1
        };

        var response =
            await _client.PostAsJsonAsync("/api/leave/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"autoApproved\":false");
    }

    [Fact]
    public async Task LongLeave_ShouldRequireManualReviewEvenWithBalanceAndStaffing()
    {
        var payload = new
        {
            daysRequested = 2.0,
            remainingLeaveBalance = 10,
            teamMembersAvailable = 5
        };

        var response =
            await _client.PostAsJsonAsync("/api/leave/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"autoApproved\":false");
    }
}
