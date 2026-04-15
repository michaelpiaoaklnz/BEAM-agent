using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class OrderTransitionPerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrderTransitionPerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Transition_Submitted_To_OnHold_WithRequiresReviewTrue_ShouldSucceed()
    {
        var payload = new
        {
            orderId = "order-101",
            currentStatus = "Submitted",
            targetStatus = "On Hold",
            requiresReview = true
        };

        var response = await _client.PostAsJsonAsync("/api/orders/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("On Hold");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Transition_Submitted_To_OnHold_WithRequiresReviewFalse_ShouldFail_With422()
    {
        var payload = new
        {
            orderId = "order-102",
            currentStatus = "Submitted",
            targetStatus = "On Hold",
            requiresReview = false
        };

        var response = await _client.PostAsJsonAsync("/api/orders/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Invalid");
    }

    [Fact]
    public async Task Transition_OnHold_To_Approved_ShouldSucceed()
    {
        var payload = new
        {
            orderId = "order-103",
            currentStatus = "On Hold",
            targetStatus = "Approved",
            requiresReview = true
        };

        var response = await _client.PostAsJsonAsync("/api/orders/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Approved");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Transition_Submitted_To_Approved_ShouldStillSucceed()
    {
        var payload = new
        {
            orderId = "order-104",
            currentStatus = "Submitted",
            targetStatus = "Approved",
            requiresReview = false
        };

        var response = await _client.PostAsJsonAsync("/api/orders/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Approved");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Transition_Invalid_OnHold_To_Issued_ShouldFail_With422()
    {
        var payload = new
        {
            orderId = "order-105",
            currentStatus = "On Hold",
            targetStatus = "Issued",
            requiresReview = true
        };

        var response = await _client.PostAsJsonAsync("/api/orders/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Invalid transition");
    }
}