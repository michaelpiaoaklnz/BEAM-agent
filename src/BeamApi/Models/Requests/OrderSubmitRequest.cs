using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class OrderSubmitRequest
{
    [Required]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    public string ShippingAddress { get; set; } = string.Empty;

    public bool ContainsFragileItems { get; set; }

    // Original T03 behavior:
    // deliveryInstructions is optional and may be omitted.
    public string? DeliveryInstructions { get; set; }
}