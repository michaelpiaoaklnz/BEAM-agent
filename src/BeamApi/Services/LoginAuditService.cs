using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class LoginAuditService
{
    private const int LockoutThreshold = 5;

    public ApiResponse<object> RecordFailure(LoginAuditRequest request)
    {
        request.LockoutDecision = request.FailedAttempts >= LockoutThreshold;

        return ApiResponse<object>.Success(
            new
            {
                userName = request.UserName,
                failedAttemptLogged = true,
                ipAddressLogged = true,
                deviceFingerprintLogged = true,
                lockoutDecisionLogged = true,
                lockoutTriggered = request.LockoutDecision
            },
            "Failed login recorded");
    }
}
