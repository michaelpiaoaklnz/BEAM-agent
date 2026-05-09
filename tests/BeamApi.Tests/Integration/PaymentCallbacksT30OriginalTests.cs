using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class PaymentCallbacksT30OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PaymentCallbacksT30OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Process_OriginalBehavior_AllowsRepeatedSideEffects()
    {
        var payload = new
        {
            transactionReference = $"txn-{Guid.NewGuid():N}",
            amount = 100m,
            existingLedgerEntries = 2,
            existingNotifications = 2
        };

        var response = await _client.PostAsJsonAsync("/api/payment-callbacks/process", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"paymentStatus\":\"Paid\"");
        body.Should().Contain("\"ledgerEntryCount\":3");
        body.Should().Contain("\"notificationCount\":3");
        body.Should().Contain("\"duplicateIgnored\":false");
    }
}
