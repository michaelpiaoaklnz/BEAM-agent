using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class OrderTransitionTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrderTransitionTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Transition_Draft_To_Submitted_ShouldSucceed()
    {
        var payload = new
        {
            orderId = "order-001",
            currentStatus = "Draft",
            targetStatus = "Submitted",
            requiresReview = false
        };

        var response = await _client.PostAsJsonAsync("/api/orders/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Submitted");
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Transition_Submitted_To_Approved_ShouldSucceed()
    {
        var payload = new
        {
            orderId = "order-002",
            currentStatus = "Submitted",
            targetStatus = "Approved",
            requiresReview = false
        };

        var response = await _client.PostAsJsonAsync("/api/orders/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Approved");
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Transition_Approved_To_Issued_ShouldSucceed()
    {
        var payload = new
        {
            orderId = "order-003",
            currentStatus = "Approved",
            targetStatus = "Issued",
            requiresReview = false
        };

        var response = await _client.PostAsJsonAsync("/api/orders/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Issued");
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Transition_InvalidTransition_ShouldFail_With422()
    {
        var payload = new
        {
            orderId = "order-004",
            currentStatus = "Draft",
            targetStatus = "Issued",
            requiresReview = false
        };

        var response = await _client.PostAsJsonAsync("/api/orders/transition", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Invalid transition");
    }
}