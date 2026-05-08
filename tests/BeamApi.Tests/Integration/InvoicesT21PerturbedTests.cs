using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class InvoicesT21PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public InvoicesT21PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(1000, 100)]
    [InlineData(1000, 999)]
    public async Task ApplyPayment_BelowInvoiceTotal_ReturnsPartiallyPaid(
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
        body.Should().Contain("Partially Paid");
    }

    [Theory]
    [InlineData(1000, 1000)]
    [InlineData(1000, 1200)]
    public async Task ApplyPayment_FullOrOverPayment_ReturnsPaid(
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
        body.Should().Contain("Paid");
    }
}