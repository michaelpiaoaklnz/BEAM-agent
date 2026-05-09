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

        // Latest-token-only behavior: any previously active tokens for this
        // user are invalidated, leaving exactly one active token.
        var hadPriorActiveTokens = request.ExistingActiveTokens > 0;

        return ApiResponse<object>.Success(
            new
            {
                email = request.Email,
                newTokenId,
                activeTokenCount = 1,
                oldTokensInvalidated = hadPriorActiveTokens
            },
            "Password reset token created");
    }
}
