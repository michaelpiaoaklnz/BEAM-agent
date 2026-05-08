using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/payment-callbacks")]
public class PaymentCallbacksController : ControllerBase
{
    private readonly PaymentCallbacksService _paymentCallbacksService;

    public PaymentCallbacksController(PaymentCallbacksService paymentCallbacksService)
    {
        _paymentCallbacksService = paymentCallbacksService;
    }

    [HttpPost("process")]
    public ActionResult<ApiResponse<object>> Process(
        [FromBody] PaymentCallbackRequest request)
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

        var result = _paymentCallbacksService.Process(request);
        return Ok(result);
    }
}