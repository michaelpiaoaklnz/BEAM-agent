using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class UserDeactivationTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UserDeactivationTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DeactivateUser_ShouldBlockLogin()
    {
        var payload = new
        {
            userId = "user-1001"
        };

        var response = await _client.PostAsJsonAsync("/api/users/deactivate", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("\"loginBlocked\":true");
        body.Should().Contain("Deactivated");
    }

    [Fact]
    public async Task DeactivateUser_MissingUserId_ShouldFail_With422()
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