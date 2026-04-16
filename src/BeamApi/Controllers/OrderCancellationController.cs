using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderCancellationController : ControllerBase
{
    private readonly OrderCancellationService _orderCancellationService;

    public OrderCancellationController(OrderCancellationService orderCancellationService)
    {
        _orderCancellationService = orderCancellationService;
    }

    [HttpPost("cancel")]
    public ActionResult<ApiResponse<object>> Cancel([FromBody] OrderCancellationRequest request)
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

        var result = _orderCancellationService.Cancel(request);
        return Ok(result);
    }
}
