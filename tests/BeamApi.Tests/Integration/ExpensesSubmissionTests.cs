using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ExpensesSubmissionTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ExpensesSubmissionTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Submit_Claim_Below500_ShouldBeAutoApproved()
    {
        var payload = new
        {
            employeeId = "emp-001",
            amount = 120.00m,
            category = "OfficeSupplies",
            hasRecentPolicyViolations = false,
            description = "Stationery"
        };

        var response = await _client.PostAsJsonAsync("/api/expenses/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("AutoApproved");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Submit_Claim_AtOrAbove500_ShouldBePendingReview()
    {
        var payload = new
        {
            employeeId = "emp-002",
            amount = 500.00m,
            category = "OfficeSupplies",
            hasRecentPolicyViolations = false,
            description = "Desk chair"
        };

        var response = await _client.PostAsJsonAsync("/api/expenses/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("PendingReview");
        body.Should().Contain("false");
    }
}