using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System;

namespace BeamApi.Services;

public class PasswordResetService
{
    private static Random _random = new Random();

    public ApiResponse<object> RequestReset(PasswordResetRequest request)
    {
        // Generate a new unique token ID
        var newTokenId = $"reset-token-{Guid.NewGuid():N}";

        return ApiResponse<object>.Success(
            new
            {
                email = request.Email,
                newTokenId,
                activeTokenCount = request.ExistingActiveTokens + 1,
                oldTokensInvalidated = false
            },
            "Password reset token created");
    }
}
