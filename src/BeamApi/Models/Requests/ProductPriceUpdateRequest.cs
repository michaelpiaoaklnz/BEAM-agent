namespace BeamApi.Models.Requests;

public class ProductPriceUpdateRequest
{
    public string ProductId { get; set; } = string.Empty;

    public decimal NewPrice { get; set; }
}
