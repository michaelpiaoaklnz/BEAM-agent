using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class UserDeactivateRequest
{
    [Required]
    public string UserId { get; set; } = string.Empty;
}