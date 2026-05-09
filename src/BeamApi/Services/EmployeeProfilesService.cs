using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class EmployeeProfilesService
{
    public ApiResponse<object> Update(EmployeeProfileUpdateRequest request)
    {
        // Check if the manager and employee are in the same department
        if (request.ManagerDepartment != request.EmployeeDepartment)
        {
            return ApiResponse<object>.Failure(
                new List<string> { "Manager cannot update employee in a different department." },
                "Department mismatch");
        }

        // Check if the request includes a salary change
        if (request.IncludesSalaryChange)
        {
            return ApiResponse<object>.Failure(
                new List<string> { "Salary changes are not allowed." },
                "Salary change not permitted");
        }

        // Original T23 behavior:
        // managers can edit employee profiles within their own department.

        return ApiResponse<object>.Success(
            new
            {
                updated = true,
                employeeId = request.EmployeeId
            },
            "Employee profile updated");
    }
}
