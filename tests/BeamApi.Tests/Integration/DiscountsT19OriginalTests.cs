using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class DiscountsT19OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DiscountsT19OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(1200, true)]
    [InlineData(1001, true)]
    [InlineData(1000, false)]
    [InlineData(800, false)]
    public async Task Evaluate_DiscountThresholdLogic_WorksCorrectly(
        decimal orderTotal,
        bool expectedDiscount)
    {
        var payload = new
        {
            orderTotal
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/discounts/evaluate",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain(
            $"\"discountApplied\":{expectedDiscount.ToString().ToLower()}");
    }
}