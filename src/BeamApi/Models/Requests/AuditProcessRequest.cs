using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class AuditProcessRequest
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string ActionName { get; set; } = string.Empty;

    [Required]
    public string EntityType { get; set; } = string.Empty;

    [Required]
    public string EntityId { get; set; } = string.Empty;
}
