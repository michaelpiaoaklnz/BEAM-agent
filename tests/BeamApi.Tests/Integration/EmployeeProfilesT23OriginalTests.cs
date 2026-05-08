using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class EmployeeProfilesT23OriginalTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EmployeeProfilesT23OriginalTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Update_AnyDepartmentUpdate_Succeeds()
    {
        var payload = new
        {
            managerDepartment = "Finance",
            employeeDepartment = "Engineering",
            includesSalaryChange = false,
            employeeId = "emp-001"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/employeeprofiles/update",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"updated\":true");
    }

    [Fact]
    public async Task Update_SalaryChange_Succeeds()
    {
        var payload = new
        {
            managerDepartment = "Finance",
            employeeDepartment = "Finance",
            includesSalaryChange = true,
            employeeId = "emp-002"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/employeeprofiles/update",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"updated\":true");
    }
}