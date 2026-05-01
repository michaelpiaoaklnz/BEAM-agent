using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class UserDeactivationService
{
    public ApiResponse<object> Deactivate(UserDeactivateRequest request)
    {
        var result = new
        {
            userId = request.UserId,
            loginBlocked = true,
            rolesRevoked = true,
            tasksReassigned = true,
            status = "Deactivated"
        };

        return ApiResponse<object>.Success(result, "User deactivated successfully");
    }
}