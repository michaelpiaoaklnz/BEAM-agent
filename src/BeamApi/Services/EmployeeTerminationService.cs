using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class EmployeeTerminationService
{
    public ApiResponse<object> Terminate(EmployeeTerminationRequest request)
    {
        return ApiResponse<object>.Success(
            new
            {
                employeeId = request.EmployeeId,
                accountDisabled = true,
                permissionsRevoked = true,
                pendingApprovalsCancelled = true,
                payrollNotified = true
            },
            "Employee terminated");
    }
}
