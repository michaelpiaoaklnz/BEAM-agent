using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System;
using System.Collections.Generic;

namespace BeamApi.Services;

public class PasswordResetService
{
    private static Random _random = new Random();
    private Dictionary<string, List<string>> _activeTokens = new Dictionary<string, List<string>>();

    public ApiResponse<object> RequestReset(PasswordResetRequest request)
    {
        // Invalidate old tokens for the user
        if (_activeTokens.ContainsKey(request.Email))
        {
            _activeTokens[request.Email].Clear();
        }

        // Generate a new unique token ID
        var newTokenId = $"reset-token-{Guid.NewGuid():N}";

        // Add the new token to the active tokens list
        if (!_activeTokens.ContainsKey(request.Email))
        {
            _activeTokens[request.Email] = new List<string>();
        }
        _activeTokens[request.Email].Add(newTokenId);

        return ApiResponse<object>.Success(
            new
            {
                email = request.Email,
                newTokenId,
                activeTokenCount = 1, // Only the latest token remains valid
                oldTokensInvalidated = true // Invalidate old tokens
            },
            "Password reset token created");
    }
}
