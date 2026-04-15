using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RefundsController : ControllerBase
{
    private readonly RefundService _refundService;

    public RefundsController(RefundService refundService)
    {
        _refundService = refundService;
    }

    [HttpPost("approve")]
    public ActionResult<ApiResponse<object>> Approve([FromBody] RefundApprovalRequest request)
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

        var result = _refundService.Approve(request);

        if (!result.Succeeded)
        {
            return StatusCode(403, result);
        }

        return Ok(result);
    }
}