using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class LeaveService
{
    public ApiResponse<object> Submit(LeaveRequest request)
    {
        bool autoApproved =
            request.DaysRequested < 2m
            && request.RemainingLeaveBalance >= request.DaysRequested
            && request.TeamMembersAvailable >= 2;

        return ApiResponse<object>.Success(
            new
            {
                autoApproved,
                daysRequested = request.DaysRequested
            },
            "Leave request evaluated");
    }
}
