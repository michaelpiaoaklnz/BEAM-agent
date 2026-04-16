using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class CaseCreateRequest
{
    [Required]
    public string CustomerId { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    // Perturbed requirement will use this.
    // Original baseline intentionally ignores it.
    [Required]
    public string IdempotencyKey { get; set; } = string.Empty;
}