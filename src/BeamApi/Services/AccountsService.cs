using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class AccountsService
{
    private readonly EmailDomainPolicy _emailDomainPolicy;

    public AccountsService(EmailDomainPolicy emailDomainPolicy)
    {
        _emailDomainPolicy = emailDomainPolicy;
    }

    public ApiResponse<string> Register(RegisterRequest request)
    {
        var status = _emailDomainPolicy.Evaluate(request.Email);

        switch (status)
        {
            case EmailDomainStatus.Disposable:
                return ApiResponse<string>.Failure(
                    new List<string> { "Disposable email domains are not allowed." },
                    "Registration failed: Disposable email domain.");

            case EmailDomainStatus.Unverified:
                return ApiResponse<string>.Failure(
                    new List<string> { "Email must belong to a verified company domain." },
                    "Registration failed: Email is not from a verified company domain.");
        }

        return ApiResponse<string>.Success(
            $"mock-user-id-for-{request.Email}",
            "User registered successfully");
    }
}
