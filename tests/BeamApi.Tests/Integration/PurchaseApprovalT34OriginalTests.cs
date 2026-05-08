using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class PurchaseApprovalT34OriginalTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PurchaseApprovalT34OriginalTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(1000)]
    [InlineData(7000)]
    public async Task Evaluate_OriginalBehavior_ManagerApprovalOnly(
        decimal amount)
    {
        var payload = new
        {
            purchaseOrderId = $"po-{Guid.NewGuid():N}",
            amount,
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
}