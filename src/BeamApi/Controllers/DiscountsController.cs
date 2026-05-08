using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountsController : ControllerBase
{
    private readonly DiscountsService _discountsService;

    public DiscountsController(DiscountsService discountsService)
    {
        _discountsService = discountsService;
    }

    [HttpPost("evaluate")]
    public ActionResult<ApiResponse<object>> Evaluate(
        [FromBody] DiscountRequest request)
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

        var result = _discountsService.CalculateDiscount(request);

        return Ok(result);
    }
}