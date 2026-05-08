using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly InvoicesService _invoicesService;

    public InvoicesController(InvoicesService invoicesService)
    {
        _invoicesService = invoicesService;
    }

    [HttpPost("payment")]
    public ActionResult<ApiResponse<object>> ApplyPayment(
        [FromBody] InvoicePaymentRequest request)
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

        var result = _invoicesService.ApplyPayment(request);
        return Ok(result);
    }
}