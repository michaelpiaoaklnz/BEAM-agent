using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/profile-audit")]
public class ProfileAuditController : ControllerBase
{
    private readonly ProfileAuditService _profileAuditService;

    public ProfileAuditController(ProfileAuditService profileAuditService)
    {
        _profileAuditService = profileAuditService;
    }

    [HttpPost("record")]
    public ActionResult<ApiResponse<object>> RecordUpdate(
        [FromBody] ProfileAuditRequest request)
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

            return Ok(ApiResponse<object>.Failure(errors, "Validation failed"));
        }

        var result = _profileAuditService.RecordUpdate(request);

        return Ok(result);
    }
}
