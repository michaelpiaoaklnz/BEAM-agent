using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrdersService _ordersService;

    public OrdersController(OrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpPost("submit")]
    public ActionResult<ApiResponse<string>> Submit([FromBody] OrderSubmitRequest request)
    {
        if (!ModelState.IsValid || !request.IsValidQuantity())
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid input." : e.ErrorMessage)
                .ToList();

            if (!request.IsValidQuantity())
            {
                errors.Add("Quantity is out of the allowed range for the specified category.");
            }

            return Ok(ApiResponse<string>.Failure(errors, "Validation failed"));
        }

        var result = _ordersService.Submit(request);
        return Ok(result);
    }
}
