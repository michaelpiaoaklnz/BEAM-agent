using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class SuppliersOnboardingPerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SuppliersOnboardingPerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Onboard_DomesticSupplier_WithoutTaxNumber_ShouldFail_With422()
    {
        var payload = new
        {
            name = "Domestic Supplier",
            countryCode = "NZ",
            isDomestic = true,
            contactEmail = "domestic@example.com"
        };

        var response = await _client.PostAsJsonAsync("/api/suppliers/onboard", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation", "the response should indicate validation failure");
        body.Should().Contain("tax", "the response should mention the taxNumber issue");
    }

    [Fact]
    public async Task Onboard_DomesticSupplier_WithInvalidTaxNumber_ShouldFail_With422()
    {
        var payload = new
        {
            name = "Domestic Supplier Invalid Tax",
            countryCode = "NZ",
            isDomestic = true,
            taxNumber = "123",
            contactEmail = "domestic-invalid@example.com"
        };

        var response = await _client.PostAsJsonAsync("/api/suppliers/onboard", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation");
        body.Should().Contain("tax");
    }

    [Fact]
    public async Task Onboard_DomesticSupplier_WithValidTaxNumber_ShouldSucceed()
    {
        var payload = new
        {
            name = "Domestic Supplier Valid Tax",
            countryCode = "NZ",
            isDomestic = true,
            taxNumber = "123456789",
            contactEmail = "domestic-valid@example.com"
        };

        var response = await _client.PostAsJsonAsync("/api/suppliers/onboard", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Onboard_NonDomesticSupplier_WithoutTaxNumber_ShouldStillSucceed()
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