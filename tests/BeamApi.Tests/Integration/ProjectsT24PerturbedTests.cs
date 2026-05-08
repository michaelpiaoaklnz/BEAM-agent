using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ProjectsT24PerturbedTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProjectsT24PerturbedTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task View_ConfidentialProject_ProjectMember_Succeeds()
    {
        var payload = new
        {
            userRole = "user",
            isProjectMember = true,
            isConfidentialProject = true,
            projectId = "project-101"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/projects/view",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"accessGranted\":true");
    }

    [Fact]
    public async Task View_ConfidentialProject_Admin_Succeeds()
    {
        var payload = new
        {
            userRole = "admin",
            isProjectMember = false,
            isConfidentialProject = true,
            projectId = "project-102"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/projects/view",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"accessGranted\":true");
    }

    [Fact]
    public async Task View_ConfidentialProject_NonMember_Fails()
    {
        var payload = new
        {
            userRole = "user",
            isProjectMember = false,
            isConfidentialProject = true,
            projectId = "project-103"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/projects/view",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"accessGranted\":false");
    }

    [Fact]
    public async Task View_PublicProject_NonMember_StillSucceeds()
    {
        var payload = new
        {
            userRole = "user",
            isProjectMember = false,
            isConfidentialProject = false,
            projectId = "project-104"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/projects/view",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"accessGranted\":true");
    }
}