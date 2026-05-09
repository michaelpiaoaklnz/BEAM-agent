using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class EmployeeProfilesService
{
    public ApiResponse<object> Update(EmployeeProfileUpdateRequest request)
    {
        if (request.IncludesSalaryChange)
        {
            return new ApiResponse<object>
            {
                Succeeded = false,
                Message = "Salary updates are not permitted",
                Errors = new List<string>
                {
                    "Salary fields are not editable by managers"
                },
                Data = new
                {
                    updated = false,
                    employeeId = request.EmployeeId
                }
            };
        }

        if (!string.Equals(
                request.ManagerDepartment,
                request.EmployeeDepartment,
                StringComparison.OrdinalIgnoreCase))
        {
            return new ApiResponse<object>
            {
                Succeeded = false,
                Message =
                    "Managers may only update employees in the same department",
                Errors = new List<string>
                {
                    "Department mismatch"
                },
                Data = new
                {
                    updated = false,
                    employeeId = request.EmployeeId
                }
            };
        }

        return ApiResponse<object>.Success(
            new
            {
                updated = true,
                employeeId = request.EmployeeId
            },
            "Employee profile updated");
    }
}