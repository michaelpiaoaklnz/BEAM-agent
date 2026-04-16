using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeamApi.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketClosureController : ControllerBase
{
    private readonly TicketClosureService _ticketClosureService;

    public TicketClosureController(TicketClosureService ticketClosureService)
    {
        _ticketClosureService = ticketClosureService;
    }

    [HttpPost("close")]
    public ActionResult<ApiResponse<object>> Close([FromBody] TicketCloseRequest request)
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

        var result = _ticketClosureService.Close(request);

        if (!result.Succeeded)
        {
            return UnprocessableEntity(result);
        }

        return Ok(result);
    }
}
