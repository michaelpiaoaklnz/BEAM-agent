using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ProjectsT24OriginalTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProjectsT24OriginalTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task View_ConfidentialProject_NonMemberAccess_Succeeds()
    {
        var payload = new
        {
            userRole = "user",
            isProjectMember = false,
            isConfidentialProject = true,
            projectId = "project-001"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/projects/view",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"accessGranted\":true");
    }

    [Fact]
    public async Task View_PublicProject_Access_Succeeds()
    {
        var payload = new
        {
            userRole = "user",
            isProjectMember = false,
            isConfidentialProject = false,
            projectId = "project-002"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/projects/view",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"accessGranted\":true");
    }
}