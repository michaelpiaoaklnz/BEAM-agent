using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class UserDeactivationPerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UserDeactivationPerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DeactivateUser_ShouldBlockLogin_RevokeRoles_AndReassignTasks()
    {
        var payload = new
        {
            userId = "user-1101"
        };

        var response = await _client.PostAsJsonAsync("/api/users/deactivate", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"loginBlocked\":true");
        body.Should().Contain("\"rolesRevoked\":true");
        body.Should().Contain("\"tasksReassigned\":true");
    }

    [Fact]
    public async Task DeactivateUser_MissingUserId_ShouldStillFail_With422()
    {
        var payload = new
        {
            userId = ""
        };

        var response = await _client.PostAsJsonAsync("/api/users/deactivate", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation");
    }
}