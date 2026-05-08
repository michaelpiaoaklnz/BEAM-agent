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
    public ActionResult<ApiResponse<object>> Update(
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

            return Ok(ApiResponse<object>.Failure(
                errors,
                "Validation failed"));
        }

        var result = _employeeProfilesService.Update(request);

        return Ok(result);
    }
}