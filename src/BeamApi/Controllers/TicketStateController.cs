using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/tickets/state")]
public class TicketStateController : ControllerBase
{
    private readonly TicketStateService _ticketStateService;

    public TicketStateController(TicketStateService ticketStateService)
    {
        _ticketStateService = ticketStateService;
    }

    [HttpPost("transition")]
    public ActionResult<ApiResponse<object>> ApplyTransition(
        [FromBody] TicketStateRequest request)
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

        if (string.IsNullOrEmpty(request.CurrentStatus) || string.IsNullOrEmpty(request.Action))
        {
            return Ok(ApiResponse<object>.Failure(new List<string> { "CurrentStatus and Action cannot be empty." }, "Invalid input."));
        }

        var result = _ticketStateService.ApplyTransition(request);
        return Ok(result);
    }
}
