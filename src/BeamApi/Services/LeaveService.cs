using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class LeaveService
{
    public ApiResponse<object> Submit(LeaveRequest request)
    {
        // Original T20 behavior:
        // leave requests below 2 days are auto-approved.
        var autoApproved = request.DaysRequested < 2m;

        return ApiResponse<object>.Success(
            new
            {
                autoApproved,
                daysRequested = request.DaysRequested
            },
            "Leave request evaluated");
    }
}