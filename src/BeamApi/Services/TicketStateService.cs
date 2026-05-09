using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class TicketStateService
{
    public ApiResponse<object> ApplyTransition(TicketStateRequest request)
    {
        var nextStatus = request.CurrentStatus;

        if (request.CurrentStatus.Equals("Open", StringComparison.OrdinalIgnoreCase)
            && request.Action.Equals("resolve", StringComparison.OrdinalIgnoreCase))
        {
            nextStatus = "Resolved";
        }
        else if (request.CurrentStatus.Equals("Resolved", StringComparison.OrdinalIgnoreCase)
                 && request.Action.Equals("close", StringComparison.OrdinalIgnoreCase))
        {
            nextStatus = "Closed";
        }
        else if (request.CurrentStatus.Equals("Closed", StringComparison.OrdinalIgnoreCase)
                 && request.Action.Equals("reopen", StringComparison.OrdinalIgnoreCase)
                 && request.HoursSinceClosed <= 48)
        {
            nextStatus = "Open";
        }

        return ApiResponse<object>.Success(
            new
            {
                ticketId = request.TicketId,
                previousStatus = request.CurrentStatus,
                action = request.Action,
                status = nextStatus
            },
            "Ticket state transition evaluated");
    }
}
