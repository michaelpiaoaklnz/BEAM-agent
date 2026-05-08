using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class LeaveService
{
    public ApiResponse<object> Submit(LeaveRequest request)
    {
        // New T20 perturbed behavior:
        // Leave requests below 2 days are auto-approved if:
        // - Remaining leave balance is greater than or equal to daysRequested
        // - At least 2 team members are available
        // Leave requests equal to or above 2 days require manual review.
        bool autoApproved = request.DaysRequested < 2m &&
                           request.RemainingLeaveBalance >= request.DaysRequested &&
                           request.TeamMembersAvailable >= 2;

        return ApiResponse<object>.Success(
            new
            {
                autoApproved,
                daysRequested = request.DaysRequested
            },
            "Leave request evaluated");
    }
}
