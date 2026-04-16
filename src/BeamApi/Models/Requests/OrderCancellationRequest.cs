using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class OrderCancellationRequest
{
    [Required]
    public string OrderId { get; set; } = string.Empty;
}