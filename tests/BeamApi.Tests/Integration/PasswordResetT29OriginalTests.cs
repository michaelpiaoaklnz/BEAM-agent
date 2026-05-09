using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class PasswordResetT29OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PasswordResetT29OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RequestReset_OriginalBehavior_CreatesAdditionalActiveToken()
    {
        var payload = new
        {
            email = $"user_{Guid.NewGuid():N}@example.com",
            existingActiveTokens = 2
        };

        var response = await _client.PostAsJsonAsync("/api/password-reset/request", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
        body.Should().NotBeNull();
        body.Succeeded.Should().BeTrue();
        body.Message.Should().Be("Password reset token created");

        var data = body.Data;
        data.Should().NotBeNull();
        data["email"].Should().Be(payload.email);
        data["newTokenId"].Should().StartWith("reset-token-");
        data["activeTokenCount"].Should().Be(payload.existingActiveTokens + 1);
        data["oldTokensInvalidated"].Should().BeFalse();
    }
}
