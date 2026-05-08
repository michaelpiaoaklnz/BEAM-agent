using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class LoginAuditT36PerturbedTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public LoginAuditT36PerturbedTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(2, false)]
    [InlineData(5, true)]
    [InlineData(8, true)]
    public async Task RecordFailure_PerturbedBehavior_LogsSecurityMetadata(
        int failedAttempts,
        bool expectedLockout)
    {
        var payload = new
        {
            userName = $"user-{Guid.NewGuid():N}",
            failedAttempts,
            ipAddress = "192.168.1.10",
            deviceFingerprint = "device-abc"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/login-audit/record-failure",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"ipAddressLogged\":true");
        body.Should().Contain("\"deviceFingerprintLogged\":true");
        body.Should().Contain("\"lockoutDecisionLogged\":true");
        body.Should().Contain(
            $"\"lockoutTriggered\":{expectedLockout.ToString().ToLower()}");
    }
}