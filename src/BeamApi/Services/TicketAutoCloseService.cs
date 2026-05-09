using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class TicketAutoCloseService
{
    public ApiResponse<object> Evaluate(TicketAutoCloseRequest request)
    {
        var isLowPriorityInactive =
            request.Priority.Equals("low", StringComparison.OrdinalIgnoreCase)
            && request.InactiveDays >= 7;

        string reason;
        bool autoClosed;

        if (request.HasUnresolvedCustomerReply)
        {
            autoClosed = false;
            reason = "Ticket has unresolved customer reply";
        }
        else if (request.HasPendingEscalation)
        {
            autoClosed = false;
            reason = "Ticket has pending escalation";
        }
        else if (isLowPriorityInactive)
        {
            autoClosed = true;
            reason = "Low-priority inactive ticket auto-closed";
        }
        else
        {
            autoClosed = false;
            reason = "Ticket remains open";
        }

        return ApiResponse<object>.Success(
            new
            {
                ticketId = request.TicketId,
                autoClosed,
                reason
            },
            "Ticket auto-close evaluated");
    }
}