namespace BeamApi.Models.Requests;

public class TicketStateRequest
{
    public string TicketId { get; set; } = string.Empty;

    public string CurrentStatus { get; set; } = "Open";

    public string Action { get; set; } = string.Empty;

    public int HoursSinceClosed { get; set; }
}
