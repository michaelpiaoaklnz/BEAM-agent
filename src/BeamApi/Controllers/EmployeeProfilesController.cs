using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/employeeprofiles")]
public class EmployeeProfilesController : ControllerBase
{
    private readonly EmployeeProfilesService _employeeProfilesService;

    public EmployeeProfilesController(
        EmployeeProfilesService employeeProfilesService)
    {
        _employeeProfilesService = employeeProfilesService;
    }

    [HttpPost("update")]
    public IActionResult Update(
        [FromBody] EmployeeProfileUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e =>
                    string.IsNullOrWhiteSpace(e.ErrorMessage)
                        ? "Invalid input."
                        : e.ErrorMessage)
                .ToList();

            return BadRequest(ApiResponse<object>.Failure(
                errors,
                "Validation failed"));
        }

        var result = _employeeProfilesService.Update(request);

        if (result.Succeeded)
        {
            return Ok(result);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.Forbidden, result);
        }
    }
}
