using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class EmployeeProfilesService
{
    public ApiResponse<object> Update(EmployeeProfileUpdateRequest request)
    {
        // Original T23 behavior:
        // managers can edit employee profiles.

        return ApiResponse<object>.Success(
            new
            {
                updated = true,
                employeeId = request.EmployeeId
            },
            "Employee profile updated");
    }
}