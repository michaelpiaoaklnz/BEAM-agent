namespace BeamApi.Models.Requests;

public class EmployeeProfileUpdateRequest
{
    public string ManagerDepartment { get; set; } = string.Empty;

    public string EmployeeDepartment { get; set; } = string.Empty;

    public bool IncludesSalaryChange { get; set; }

    public string EmployeeId { get; set; } = string.Empty;
}