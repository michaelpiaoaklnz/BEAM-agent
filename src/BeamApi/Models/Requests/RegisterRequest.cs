using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

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

    // List of verified company domains
    public static readonly HashSet<string> VerifiedCompanyDomains = new HashSet<string>
    {
        "company.com",
        "beam.com",
        "enterprise.co.nz"
    };
}
