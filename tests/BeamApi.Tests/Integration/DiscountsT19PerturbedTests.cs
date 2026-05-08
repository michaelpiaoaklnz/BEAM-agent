using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class DiscountsT19PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DiscountsT19PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PlatinumCustomer_Above500_ShouldReceiveDiscount()
    {
        var payload = new
        {
            orderTotal = 600,
            customerTier = "platinum",
            previousOrders = 1,
            productCategory = "general"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/discounts/evaluate",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"discountApplied\":true");
    }

    [Fact]
    public async Task GoldCustomer_WithEnoughOrders_ShouldReceiveDiscount()
    {
        var payload = new
        {
            orderTotal = 900,
            customerTier = "gold",
            previousOrders = 7,
            productCategory = "general"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/discounts/evaluate",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"discountApplied\":true");
    }

    [Fact]
    public async Task GoldCustomer_WithoutEnoughOrders_ShouldFail()
    {
        var payload = new
        {
            orderTotal = 900,
            customerTier = "gold",
            previousOrders = 1,
            productCategory = "general"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/discounts/evaluate",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"discountApplied\":false");
    }

    [Fact]
    public async Task RestrictedCategory_ShouldNeverReceiveDiscount()
    {
        var payload = new
        {
            orderTotal = 5000,
            customerTier = "platinum",
            previousOrders = 50,
            productCategory = "alcohol"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/discounts/evaluate",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"discountApplied\":false");
    }

    [Fact]
    public async Task StandardCustomer_Above1200_ShouldReceiveDiscount()
    {
        var payload = new
        {
            orderTotal = 1300,
            customerTier = "standard",
            previousOrders = 0,
            productCategory = "general"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/discounts/evaluate",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"discountApplied\":true");
    }

    [Fact]
    public async Task StandardCustomer_Below1200_ShouldFail()
    {
        var payload = new
        {
            orderTotal = 1100,
            customerTier = "standard",
            previousOrders = 0,
            productCategory = "general"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/discounts/evaluate",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"discountApplied\":false");
    }
}
