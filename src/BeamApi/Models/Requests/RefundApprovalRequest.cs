using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class RefundApprovalRequest
{
    [Required]
    public string UserRole { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public string RefundId { get; set; } = string.Empty;
}