using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProfileAuditService
{
    public ApiResponse<object> RecordUpdate(ProfileAuditRequest request)
    {
        // Original T35 behavior:
        // profile updates log user ID and timestamp only.

        return ApiResponse<object>.Success(
            new
            {
                userId = request.UserId,
                timestampLogged = true,
                beforeAfterValuesLogged = false,
                actingUserRoleLogged = false,
                requestSourceLogged = false
            },
            "Profile update audit logged");
    }
}