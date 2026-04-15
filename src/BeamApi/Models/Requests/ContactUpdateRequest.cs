using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class ContactUpdateRequest
{
    [Required]
    public string CustomerId { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
}