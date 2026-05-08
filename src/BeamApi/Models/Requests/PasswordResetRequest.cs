namespace BeamApi.Models.Requests;

public class PasswordResetRequest
{
    public string Email { get; set; } = string.Empty;

    public int ExistingActiveTokens { get; set; }
}