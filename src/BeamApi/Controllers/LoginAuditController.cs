using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/login-audit")]
public class LoginAuditController : ControllerBase
{
    private readonly LoginAuditService _loginAuditService;

    public LoginAuditController(LoginAuditService loginAuditService)
    {
        _loginAuditService = loginAuditService;
    }

    [HttpPost("record-failure")]
    public ActionResult<ApiResponse<object>> RecordFailure(
        [FromBody] LoginAuditRequest request)
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

        var result = _loginAuditService.RecordFailure(request);

        return Ok(result);
    }
}