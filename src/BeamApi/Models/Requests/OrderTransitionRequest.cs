using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class OrderTransitionRequest
{
    [Required]
    public string OrderId { get; set; } = string.Empty;

    [Required]
    public string CurrentStatus { get; set; } = string.Empty;

    [Required]
    public string TargetStatus { get; set; } = string.Empty;

    // Perturbed requirement will use this.
    // Original baseline intentionally ignores it.
    public bool RequiresReview { get; set; }
}