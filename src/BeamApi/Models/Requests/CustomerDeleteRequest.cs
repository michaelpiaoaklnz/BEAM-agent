namespace BeamApi.Models.Requests;

public class CustomerDeleteRequest
{
    public string CustomerId { get; set; } = string.Empty;

    public bool HasRelatedOrders { get; set; }

    public bool HasActiveDisputes { get; set; }
}