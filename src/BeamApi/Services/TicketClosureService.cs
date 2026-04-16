using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class TicketClosureService
{
    public ApiResponse<object> Close(TicketCloseRequest request)
    {
        // Original T11 baseline behavior:
        // tickets auto-close after 7 days, regardless of priority.
        var closed = request.DaysOpen >= 7;

        if (!closed)
        {
            return ApiResponse<object>.Failure(
                new List<string> { "Ticket is not eligible for closure yet." },
                "Ticket closure failed");
        }

        var result = new
        {
            ticketId = request.TicketId,
            status = "Closed",
            closureMode = "Auto"
        };

        return ApiResponse<object>.Success(result, "Ticket closed successfully");
    }
}