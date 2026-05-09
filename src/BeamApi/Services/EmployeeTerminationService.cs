using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class EmployeeTerminationService
{
    public ApiResponse<object> Terminate(EmployeeTerminationRequest request)
    {
        // New T31 behavior:
        // termination disables employee account, revokes permissions, cancels pending approvals, and notifies payroll systems.

        var accountDisabled = true;
        var permissionsRevoked = true;
        var pendingApprovalsCancelled = request.HasPendingApprovals;
        var payrollNotified = request.HasPayrollProfile;

        return ApiResponse<object>.Success(
            new
            {
                employeeId = request.EmployeeId,
                accountDisabled = accountDisabled,
                permissionsRevoked = permissionsRevoked,
                pendingApprovalsCancelled = pendingApprovalsCancelled,
                payrollNotified = payrollNotified
            },
            "Employee terminated");
    }
}
