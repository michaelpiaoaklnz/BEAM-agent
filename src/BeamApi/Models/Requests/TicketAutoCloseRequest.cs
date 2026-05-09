namespace BeamApi.Models.Requests;

public class TicketAutoCloseRequest
{
    public string TicketId { get; set; } = string.Empty;

    public string Priority { get; set; } = "low";

    public int InactiveDays { get; set; }

    public bool HasUnresolvedCustomerReply { get; set; }

    public bool HasPendingEscalation { get; set; }
}
