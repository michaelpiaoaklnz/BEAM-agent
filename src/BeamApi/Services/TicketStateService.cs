using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class TicketStateService
{
    public ApiResponse<object> ApplyTransition(TicketStateRequest request)
    {
        // Original T22 behavior:
        // support ticket moves Open -> Resolved -> Closed.
        // Reopening behavior is not supported in the original requirement.

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
                 && request.Action.Equals("reopen", StringComparison.OrdinalIgnoreCase))
        {
            nextStatus = request.HoursSinceClosed <= 48 ? "Open" : "Closed";
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
