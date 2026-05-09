using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/product-prices")]
public class ProductPricesController : ControllerBase
{
    private readonly ProductPricesService _productPricesService;

    public ProductPricesController(ProductPricesService productPricesService)
    {
        _productPricesService = productPricesService;
    }

    [HttpPost("update")]
    public ActionResult<ApiResponse<object>> UpdatePrice(
        [FromBody] ProductPriceUpdateRequest request)
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

            return Ok(ApiResponse<object>.Failure(errors, "Validation failed"));
        }

        var result = _productPricesService.UpdatePrice(request);

        return Ok(result);
    }
}
