using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class AccountsService
{
    public ApiResponse<string> Register(RegisterRequest request)
    {
        // Simulate user registration logic
        // For demonstration purposes, we'll just return a success response
        return ApiResponse<string>.Success(
            $"mock-user-id-for-{request.Email}",
            "User registered successfully");
    }
}
