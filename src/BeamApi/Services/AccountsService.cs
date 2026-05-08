using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Linq;

namespace BeamApi.Services;

public class AccountsService
{
    public ApiResponse<string> Register(RegisterRequest request)
    {
        // Extract the domain from the email
        var domain = request.Email.Split('@')[1];

        // Check if the domain is disposable
        if (IsDisposableDomain(domain))
        {
            return ApiResponse<string>.Failure(
                new[] { "Disposable email domain is not allowed." },
                "Registration failed");
        }

        // Check if the domain is a verified company domain
        if (!RegisterRequest.VerifiedCompanyDomains.Contains(domain))
        {
            return ApiResponse<string>.Failure(
                new[] { "Unverified company domain is not allowed." },
                "Registration failed");
        }

        // Simulate successful registration
        return ApiResponse<string>.Success(
            $"mock-user-id-for-{request.Email}",
            "User registered successfully");
    }

    private bool IsDisposableDomain(string domain)
    {
        // List of disposable domains
        var disposableDomains = new HashSet<string>
        {
            "mailinator.com",
            "tempmail.com",
            "10minutemail.com",
            "guerrillamail.com"
        };

        return disposableDomains.Contains(domain);
    }
}
