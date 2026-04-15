using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

    /// <summary>
    /// Approves a refund request.
    /// </summary>
    /// <param name="request">The refund approval request.</param>
    /// <returns>An ActionResult containing the ApiResponse.</returns>
    [HttpPost("approve")]
    public ActionResult<ApiResponse<object>> Approve([FromBody] RefundApprovalRequest request)
    {
        // Validate the incoming request
        if (!ModelState.IsValid)
        {
            // Collect validation errors
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid input." : e.ErrorMessage)
                .ToList();

            // Return unprocessable entity with validation errors
            return UnprocessableEntity(ApiResponse<object>.Failure(errors, "Validation failed"));
        }

        // Call the RefundService to approve the refund
        var result = _refundService.Approve(request);

        // Check if the approval was successful
        if (!result.Succeeded)
        {
            // Return forbidden if the user is not authorized
            return StatusCode(403, result);
        }

        // Return OK with the success response
        return Ok(result);
    }
}
