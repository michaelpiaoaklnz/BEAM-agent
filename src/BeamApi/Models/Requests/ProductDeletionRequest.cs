namespace BeamApi.Models.Requests;

public class ProductDeletionRequest
{
    public string ProductId { get; set; } = string.Empty;

    public bool HasExistingOrders { get; set; }
}