using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class AccountsService
{
    public ApiResponse<string> Register(RegisterRequest request)
    {
        return ApiResponse<string>.Success(
            $"mock-user-id-for-{request.Email}",
            "User registered successfully");
    }
}