using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserDeactivationController : ControllerBase
{
    private readonly UserDeactivationService _userDeactivationService;

    public UserDeactivationController(UserDeactivationService userDeactivationService)
    {
        _userDeactivationService = userDeactivationService;
    }

    [HttpPost("deactivate")]
    public ActionResult<ApiResponse<object>> Deactivate([FromBody] UserDeactivateRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid input." : e.ErrorMessage)
                .ToList();

            return UnprocessableEntity(ApiResponse<object>.Failure(errors, "Validation failed"));
        }

        var result = _userDeactivationService.Deactivate(request);
        return Ok(result);
    }
}
