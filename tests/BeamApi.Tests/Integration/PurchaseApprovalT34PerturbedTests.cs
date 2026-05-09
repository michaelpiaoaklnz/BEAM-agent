using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class PurchaseApprovalT34PerturbedTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PurchaseApprovalT34PerturbedTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Evaluate_LowValueOrder_ManagerApprovalOnly_Succeeds()
    {
        var payload = new
        {
            purchaseOrderId = $"po-{Guid.NewGuid():N}",
            amount = 3000m,
            managerApproved = true,
            financeApproved = false
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/purchase-approval/evaluate",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"approved\":true");
        body.Should().Contain("\"financeApprovalRequired\":false");
    }

    [Fact]
    public async Task Evaluate_HighValueOrder_WithoutFinanceApproval_Fails()
    {
        var payload = new
        {
            purchaseOrderId = $"po-{Guid.NewGuid():N}",
            amount = 7000m,
            managerApproved = true,
            financeApproved = false
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/purchase-approval/evaluate",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"approved\":false");
        body.Should().Contain("\"financeApprovalRequired\":true");
    }

    [Fact]
    public async Task Evaluate_HighValueOrder_WithFinanceApproval_Succeeds()
    {
        var payload = new
        {
            purchaseOrderId = $"po-{Guid.NewGuid():N}",
            amount = 7000m,
            managerApproved = true,
            financeApproved = true
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/purchase-approval/evaluate",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"approved\":true");
        body.Should().Contain("\"financeApprovalRequired\":true");
    }
}
