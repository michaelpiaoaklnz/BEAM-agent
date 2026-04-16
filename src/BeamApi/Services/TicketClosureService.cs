using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class TicketClosureService
{
    public ApiResponse<object> Close(TicketCloseRequest request)
    {
        // Check if the ticket is older than 7 days
        bool isOlderThan7Days = request.DaysOpen >= 7;

        // Check if the ticket is priority and manual closure is not requested
        bool isPriorityWithoutManualClosure = request.IsPriority && !request.ManualClosureRequested;

        if (isPriorityWithoutManualClosure)
        {
            return ApiResponse<object>.Failure(
                new List<string> { "Priority tickets require manual closure." },
                "Ticket closure failed");
        }

        // Allow non-priority tickets to auto-close after 7 days
        if (isOlderThan7Days && !request.IsPriority)
        {
            var result = new
            {
                ticketId = request.TicketId,
                status = "Closed",
                closureMode = "Auto"
            };

            return ApiResponse<object>.Success(result, "Ticket closed successfully");
        }

        // Allow priority tickets to close if manual closure is requested
        if (isOlderThan7Days && request.IsPriority && request.ManualClosureRequested)
        {
            var result = new
            {
                ticketId = request.TicketId,
                status = "Closed",
                closureMode = "Manual"
            };

            return ApiResponse<object>.Success(result, "Ticket closed successfully");
        }

        // If none of the conditions are met, the ticket cannot be closed
        return ApiResponse<object>.Failure(
            new List<string> { "Ticket is not eligible for closure yet." },
            "Ticket closure failed");
    }
}
