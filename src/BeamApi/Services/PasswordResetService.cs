using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class PasswordResetService
{
    public ApiResponse<object> RequestReset(PasswordResetRequest request)
    {
        // Original T29 behavior:
        // multiple password reset requests create multiple reset tokens.
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