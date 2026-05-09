using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class PasswordResetT29PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PasswordResetT29PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RequestReset_PerturbedBehavior_InvalidatesOldTokensAndKeepsOnlyLatest()
    {
        var payload = new
        {
            email = $"user_{Guid.NewGuid():N}@example.com",
            existingActiveTokens = 4
        };

        var response = await _client.PostAsJsonAsync("/api/password-reset/request", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("reset-token-");
        body.Should().Contain("\"activeTokenCount\":1");
        body.Should().Contain("\"oldTokensInvalidated\":true");
    }
}
