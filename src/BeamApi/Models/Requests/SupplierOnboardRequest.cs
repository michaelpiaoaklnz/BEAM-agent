using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class SupplierOnboardRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string CountryCode { get; set; } = string.Empty;

    /// <summary>
    /// Original T02 behavior:
    /// domestic suppliers are allowed to omit taxNumber.
    /// </summary>
    public bool IsDomestic { get; set; }

    // Original requirement: optional for domestic suppliers.
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Tax number must consist of exactly 9 digits.")]
    public string? TaxNumber { get; set; }

    [Required]
    [EmailAddress]
    public string ContactEmail { get; set; } = string.Empty;
}
