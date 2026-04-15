using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderWorkflowController : ControllerBase
{
    private readonly OrderWorkflowService _orderWorkflowService;

    public OrderWorkflowController(OrderWorkflowService orderWorkflowService)
    {
        _orderWorkflowService = orderWorkflowService;
    }

    [HttpPost("transition")]
    public ActionResult<ApiResponse<object>> Transition([FromBody] OrderTransitionRequest request)
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

        var result = _orderWorkflowService.Transition(request);

        if (!result.Succeeded)
        {
            return UnprocessableEntity(result);
        }

        return Ok(result);
    }
}