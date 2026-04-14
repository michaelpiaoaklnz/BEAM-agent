using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class SuppliersOnboardingTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SuppliersOnboardingTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Onboard_DomesticSupplier_WithoutTaxNumber_ShouldSucceed()
    {
        var payload = new
        {
            name = "Domestic Supplier",
            countryCode = "NZ",
            isDomestic = true,
            contactEmail = "domestic@example.com"
        };

        var response = await _client.PostAsJsonAsync("/api/suppliers/onboard", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded", "the API should return the standard response shape");
        body.Should().Contain("true", "a valid original request should succeed");
    }

    [Fact]
    public async Task Onboard_DomesticSupplier_WithTaxNumber_ShouldSucceed()
    {
        var payload = new
        {
            name = "Domestic Supplier With Tax",
            countryCode = "NZ",
            isDomestic = true,
            taxNumber = "ABC123",
            contactEmail = "domestic-tax@example.com"
        };

        var response = await _client.PostAsJsonAsync("/api/suppliers/onboard", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Onboard_NonDomesticSupplier_WithoutTaxNumber_ShouldSucceed()
    {
        var payload = new
        {
            name = "International Supplier",
            countryCode = "US",
            isDomestic = false,
            contactEmail = "international@example.com"
        };

        var response = await _client.PostAsJsonAsync("/api/suppliers/onboard", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }
}