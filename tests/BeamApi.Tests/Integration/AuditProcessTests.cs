using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class AuditProcessTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuditProcessTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ProcessAudit_ShouldIncludeBasicAuditFields()
    {
        var payload = new
        {
            userId = "user-2001",
            actionName = "UpdateAccount",
            entityType = "Account",
            entityId = "acc-001"
        };

        var response = await _client.PostAsJsonAsync("/api/audit/process", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"processed\":true");
        body.Should().Contain("\"userId\":\"user-2001\"");
        body.Should().Contain("timestamp");
    }

    [Fact]
    public async Task ProcessAudit_MissingUserId_ShouldFail_With422()
    {
        var payload = new
        {
            userId = "",
            actionName = "UpdateAccount",
            entityType = "Account",
            entityId = "acc-002"
        };

        var response = await _client.PostAsJsonAsync("/api/audit/process", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation");
    }
}