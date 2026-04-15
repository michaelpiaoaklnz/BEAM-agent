using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ExpensesSubmissionPerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ExpensesSubmissionPerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Submit_Claim_Below500_NoViolations_AllowedCategory_ShouldBeAutoApproved()
    {
        var payload = new
        {
            employeeId = "emp-101",
            amount = 120.00m,
            category = "OfficeSupplies",
            hasRecentPolicyViolations = false,
            description = "Pens"
        };

        var response = await _client.PostAsJsonAsync("/api/expenses/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("AutoApproved");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Submit_Claim_Below500_WithViolations_ShouldNotBeAutoApproved()
    {
        var payload = new
        {
            employeeId = "emp-102",
            amount = 120.00m,
            category = "OfficeSupplies",
            hasRecentPolicyViolations = true,
            description = "Mouse"
        };

        var response = await _client.PostAsJsonAsync("/api/expenses/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("PendingReview");
        body.Should().Contain("false");
    }

    [Fact]
    public async Task Submit_Claim_Below500_RestrictedCategory_ShouldNotBeAutoApproved()
    {
        var payload = new
        {
            employeeId = "emp-103",
            amount = 120.00m,
            category = "Travel",
            hasRecentPolicyViolations = false,
            description = "Taxi fare"
        };

        var response = await _client.PostAsJsonAsync("/api/expenses/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("PendingReview");
        body.Should().Contain("false");
    }

    [Fact]
    public async Task Submit_Claim_AtOrAbove500_ShouldNotBeAutoApproved()
    {
        var payload = new
        {
            employeeId = "emp-104",
            amount = 700.00m,
            category = "OfficeSupplies",
            hasRecentPolicyViolations = false,
            description = "Monitor"
        };

        var response = await _client.PostAsJsonAsync("/api/expenses/submit", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("PendingReview");
        body.Should().Contain("false");
    }
}