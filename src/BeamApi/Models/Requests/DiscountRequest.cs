namespace BeamApi.Models.Requests;

public class DiscountRequest
{
    public decimal OrderTotal { get; set; }

    public string CustomerTier { get; set; } = "standard";

    public int PreviousOrders { get; set; }

    public string ProductCategory { get; set; } = "general";
}