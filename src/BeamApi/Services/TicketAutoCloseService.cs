using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class TicketAutoCloseService
{
    public ApiResponse<object> Evaluate(TicketAutoCloseRequest request)
    {
        // Check for exceptions
        if (request.HasUnresolvedCustomerReply || request.HasPendingEscalation)
        {
            return ApiResponse<object>.Success(
                new
                {
                    ticketId = request.TicketId,
                    autoClosed = false,
                    reason = "Ticket has unresolved customer reply or pending escalation"
                },
                "Ticket auto-close evaluated");
        }

        // Original T33 behavior:
        // all low-priority tickets auto-close after inactivity.
        var autoClosed =
            request.Priority.Equals("low", StringComparison.OrdinalIgnoreCase)
            && request.InactiveDays >= 7;

        return ApiResponse<object>.Success(
            new
            {
                ticketId = request.TicketId,
                autoClosed,
                reason = autoClosed ? "Low-priority inactive ticket auto-closed" : "Ticket remains open"
            },
            "Ticket auto-close evaluated");
    }
}
