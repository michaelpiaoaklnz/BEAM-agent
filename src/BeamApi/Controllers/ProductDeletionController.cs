using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/product-deletion")]
public class ProductDeletionController : ControllerBase
{
    private readonly ProductDeletionService _productDeletionService;

    public ProductDeletionController(
        ProductDeletionService productDeletionService)
    {
        _productDeletionService = productDeletionService;
    }

    [HttpPost("delete")]
    public ActionResult<ApiResponse<object>> Delete(
        [FromBody] ProductDeletionRequest request)
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

        var result = _productDeletionService.Delete(request);

        return Ok(result);
    }
}