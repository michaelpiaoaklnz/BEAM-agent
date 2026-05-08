using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/tickets/autoclose")]
public class TicketAutoCloseController : ControllerBase
{
    private readonly TicketAutoCloseService _ticketAutoCloseService;

    public TicketAutoCloseController(TicketAutoCloseService ticketAutoCloseService)
    {
        _ticketAutoCloseService = ticketAutoCloseService;
    }

    [HttpPost("evaluate")]
    public ActionResult<ApiResponse<object>> Evaluate(
        [FromBody] TicketAutoCloseRequest request)
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

        var result = _ticketAutoCloseService.Evaluate(request);
        return Ok(result);
    }
}