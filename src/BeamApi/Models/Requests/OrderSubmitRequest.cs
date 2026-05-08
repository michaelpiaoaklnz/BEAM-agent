using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class OrderSubmitRequest
{
    [Required]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    public string ShippingAddress { get; set; } = string.Empty;

    // Existing T03 field
    public bool ContainsFragileItems { get; set; }

    // T14 fields

    [Required]
    public string ProductId { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public string? DeliveryInstructions { get; set; }
}