using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class RefundApprovalPerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public RefundApprovalPerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ApproveRefund_Admin_ShouldStillSucceed()
    {
        var payload = new
        {
            userRole = "Admin",
            amount = 2500.00m,
            refundId = "refund-101"
        };

        var response = await _client.PostAsJsonAsync("/api/refunds/approve", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Approved");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task ApproveRefund_TeamLead_AtOrBelow1000_ShouldSucceed()
    {
        var payload = new
        {
            userRole = "TeamLead",
            amount = 750.00m,
            refundId = "refund-102"
        };

        var response = await _client.PostAsJsonAsync("/api/refunds/approve", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Approved");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task ApproveRefund_TeamLead_Above1000_ShouldFail_With403()
    {
        var payload = new
        {
            userRole = "TeamLead",
            amount = 1500.00m,
            refundId = "refund-103"
        };

        var response = await _client.PostAsJsonAsync("/api/refunds/approve", payload);

        response.StatusCode.Should().Be((HttpStatusCode)403);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("not authorized");
    }

    [Fact]
    public async Task ApproveRefund_Staff_ShouldFail_With403()
    {
        var payload = new
        {
            userRole = "Staff",
            amount = 200.00m,
            refundId = "refund-104"
        };

        var response = await _client.PostAsJsonAsync("/api/refunds/approve", payload);

        response.StatusCode.Should().Be((HttpStatusCode)403);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("not authorized");
    }
}