using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/employee-termination")]
public class EmployeeTerminationController : ControllerBase
{
    private readonly EmployeeTerminationService _employeeTerminationService;

    public EmployeeTerminationController(
        EmployeeTerminationService employeeTerminationService)
    {
        _employeeTerminationService = employeeTerminationService;
    }

    [HttpPost("terminate")]
    public ActionResult<ApiResponse<object>> Terminate(
        [FromBody] EmployeeTerminationRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e =>
                    string.IsNullOrWhiteSpace(e.ErrorMessage)
                        ? "Invalid input."
                        : e.ErrorMessage)
                .ToList();

            return Ok(ApiResponse<object>.Failure(
                errors,
                "Validation failed"));
        }

        var result = _employeeTerminationService.Terminate(request);

        return Ok(result);
    }
}
