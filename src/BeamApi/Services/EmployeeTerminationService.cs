using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class EmployeeTerminationService
{
    public ApiResponse<object> Terminate(EmployeeTerminationRequest request)
    {
        // Original T31 behavior:
        // termination disables employee account only.

        return ApiResponse<object>.Success(
            new
            {
                employeeId = request.EmployeeId,
                accountDisabled = true,
                permissionsRevoked = false,
                pendingApprovalsCancelled = false,
                payrollNotified = false
            },
            "Employee terminated");
    }
}  