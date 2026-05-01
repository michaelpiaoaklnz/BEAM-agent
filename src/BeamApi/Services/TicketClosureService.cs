using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class TicketClosureService
{
    public ApiResponse<object> Close(TicketCloseRequest request)
    {
        if (request.DaysOpen < 7 && !request.ManualClosureRequested)
        {
            return ApiResponse<object>.Failure(
                new List<string> { "Ticket is not eligible for closure yet." },
                "Ticket closure failed");
        }

        if (request.IsPriority && !request.ManualClosureRequested)
        {
            return ApiResponse<object>.Failure(
                new List<string> { "Priority tickets require manual closure." },
                "Ticket closure failed");
        }

        var closureMode = request.ManualClosureRequested ? "Manual" : "Auto";

        var result = new
        {
            ticketId = request.TicketId,
            status = "Closed",
            closureMode
        };

        return ApiResponse<object>.Success(result, "Ticket closed successfully");
    }
}
