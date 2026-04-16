using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class AuditProcessPerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuditProcessPerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ProcessAudit_ShouldIncludeFullAuditDetails()
    {
        var payload = new
        {
            userId = "user-2101",
            actionName = "DeactivateUser",
            entityType = "User",
            entityId = "usr-001"
        };

        var response = await _client.PostAsJsonAsync("/api/audit/process", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"userId\":\"user-2101\"");
        body.Should().Contain("timestamp");
        body.Should().Contain("\"actionName\":\"DeactivateUser\"");
        body.Should().Contain("\"entityType\":\"User\"");
        body.Should().Contain("\"entityId\":\"usr-001\"");
        body.Should().Contain("\"outcome\":\"Success\"");
    }

    [Fact]
    public async Task ProcessAudit_MissingUserId_ShouldStillFail_With422()
    {
        var payload = new
        {
            userId = "",
            actionName = "DeactivateUser",
            entityType = "User",
            entityId = "usr-002"
        };

        var response = await _client.PostAsJsonAsync("/api/audit/process", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation");
    }
}