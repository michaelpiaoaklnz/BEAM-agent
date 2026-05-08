namespace BeamApi.Models.Requests;

public class EmployeeTerminationRequest
{
    public string EmployeeId { get; set; } = string.Empty;

    public bool HasPendingApprovals { get; set; }

    public bool HasPayrollProfile { get; set; }
}