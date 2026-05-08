using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class EmployeeProfilesT23PerturbedTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EmployeeProfilesT23PerturbedTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Update_SameDepartmentWithoutSalaryChange_Succeeds()
    {
        var payload = new
        {
            managerDepartment = "Finance",
            employeeDepartment = "Finance",
            includesSalaryChange = false,
            employeeId = "emp-101"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/employeeprofiles/update",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"updated\":true");
    }

    [Fact]
    public async Task Update_DifferentDepartment_Fails()
    {
        var payload = new
        {
            managerDepartment = "Finance",
            employeeDepartment = "Engineering",
            includesSalaryChange = false,
            employeeId = "emp-102"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/employeeprofiles/update",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"updated\":false");
    }

    [Fact]
    public async Task Update_SalaryChange_Fails()
    {
        var payload = new
        {
            managerDepartment = "Finance",
            employeeDepartment = "Finance",
            includesSalaryChange = true,
            employeeId = "emp-103"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/employeeprofiles/update",
                payload);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"updated\":false");
    }
}