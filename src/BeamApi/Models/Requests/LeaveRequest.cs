namespace BeamApi.Models.Requests;

public class LeaveRequest
{
    public decimal DaysRequested { get; set; }

    public decimal RemainingLeaveBalance { get; set; }

    public int TeamMembersAvailable { get; set; }
}
