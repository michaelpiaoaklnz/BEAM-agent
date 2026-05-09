using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/password-reset")]
public class PasswordResetController : ControllerBase
{
    private readonly PasswordResetService _passwordResetService;

    public PasswordResetController(PasswordResetService passwordResetService)
    {
        _passwordResetService = passwordResetService;
    }

    [HttpPost("request")]
    public ActionResult<ApiResponse<object>> RequestReset(
        [FromBody] PasswordResetRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                    ? "Invalid input."
                    : e.ErrorMessage)
                .ToList();

            return Ok(ApiResponse<object>.Failure(errors, "Validation failed"));
        }

        var result = _passwordResetService.RequestReset(request);
        return Ok(result);
    }
}
