using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class RefundApprovalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public RefundApprovalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ApproveRefund_Admin_ShouldSucceed()
    {
        var payload = new
        {
            userRole = "Admin",
            amount = 1500.00m,
            refundId = "refund-001"
        };

        var response = await _client.PostAsJsonAsync("/api/refunds/approve", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Approved");
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task ApproveRefund_NonAdmin_ShouldFail_With403()
    {
        var payload = new
        {
            userRole = "Staff",
            amount = 100.00m,
            refundId = "refund-002"
        };

        var response = await _client.PostAsJsonAsync("/api/refunds/approve", payload);

        response.StatusCode.Should().Be((HttpStatusCode)403);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("not authorized");
    }
}