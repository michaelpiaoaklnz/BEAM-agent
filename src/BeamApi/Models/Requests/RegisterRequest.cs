using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

    public bool IsFieldStaff { get; set; }

    [RequiredIf("IsFieldStaff", true, ErrorMessage = "Emergency contact phone is required for field staff.")]
    public string? EmergencyContactPhone { get; set; }

    [RequiredIf("IsFieldStaff", true, ErrorMessage = "Emergency contact relationship is required for field staff.")]
    public string? EmergencyContactRelationship { get; set; }
}
