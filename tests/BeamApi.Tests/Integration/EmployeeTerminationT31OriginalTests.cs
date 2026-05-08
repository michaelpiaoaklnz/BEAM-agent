using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class EmployeeTerminationT31OriginalTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EmployeeTerminationT31OriginalTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Terminate_OriginalBehavior_DisablesAccountOnly()
    {
        var payload = new
        {
            employeeId = $"emp-{Guid.NewGuid():N}",
            hasPendingApprovals = true,
            hasPayrollProfile = true
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/employee-termination/terminate",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"accountDisabled\":true");
        body.Should().Contain("\"permissionsRevoked\":false");
        body.Should().Contain("\"pendingApprovalsCancelled\":false");
        body.Should().Contain("\"payrollNotified\":false");
    }
}