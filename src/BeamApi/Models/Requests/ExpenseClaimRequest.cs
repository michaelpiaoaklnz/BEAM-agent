using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class ExpenseClaimRequest
{
    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    // Perturbed requirement will use this,
    // but original baseline intentionally ignores it.
    public bool HasRecentPolicyViolations { get; set; }

    public string? Description { get; set; }
}