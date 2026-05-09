using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/purchase-approval")]
public class PurchaseApprovalController : ControllerBase
{
    private readonly PurchaseApprovalService _purchaseApprovalService;

    public PurchaseApprovalController(
        PurchaseApprovalService purchaseApprovalService)
    {
        _purchaseApprovalService = purchaseApprovalService;
    }

    [HttpPost("evaluate")]
    public ActionResult<ApiResponse<object>> Evaluate(
        [FromBody] PurchaseApprovalRequest request)
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

        var result = _purchaseApprovalService.Evaluate(request);

        return Ok(result);
    }
}
