using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class AccountsService
{
    private readonly HashSet<string> _usedPasswords = new();

    public ApiResponse<string> Register(RegisterRequest request)
    {
        if (_usedPasswords.Contains(request.Password))
            return ApiResponse<string>.Failure(
                new List<string> { "Password has been used recently." },
                "Validation failed");

        _usedPasswords.Add(request.Password);
        return ApiResponse<string>.Success(
            "mock-user-id",
            "User Registered (mock for experiment)");
    }
}