using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class UserDeactivationService
{
    public ApiResponse<object> Deactivate(UserDeactivateRequest request)
    {
        // Original T10 baseline behavior:
        // only block login.
        var result = new
        {
            userId = request.UserId,
            loginBlocked = true,
            rolesRevoked = false,
            tasksReassigned = false,
            status = "Deactivated"
        };

        return ApiResponse<object>.Success(result, "User deactivated successfully");
    }
}