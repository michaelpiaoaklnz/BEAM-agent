using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class UserDeactivationService
{
    public ApiResponse<object> Deactivate(UserDeactivateRequest request)
    {
        // Simulate revoking roles and reassigning tasks
        var rolesRevoked = true;
        var tasksReassigned = true;

        var result = new
        {
            userId = request.UserId,
            loginBlocked = true,
            rolesRevoked = rolesRevoked,
            tasksReassigned = tasksReassigned,
            status = "Deactivated"
        };

        return ApiResponse<object>.Success(result, "User deactivated successfully");
    }
}
