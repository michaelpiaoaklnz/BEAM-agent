using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class InvoicesT21OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public InvoicesT21OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(1000, 1000)]
    [InlineData(1000, 500)]
    [InlineData(1000, 1500)]
    public async Task ApplyPayment_OriginalBehavior_AlwaysMovesToPaid(
        decimal invoiceTotal,
        decimal paymentAmount)
    {
        var payload = new
        {
            invoiceId = $"invoice-{Guid.NewGuid():N}",
            invoiceTotal,
            paymentAmount
        };

        var response =
            await _client.PostAsJsonAsync("/api/invoices/payment", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
        body.Should().Contain("Paid");
    }
}