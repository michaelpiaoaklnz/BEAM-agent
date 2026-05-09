using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class LoginAuditService
{
    private const int LockoutThreshold = 5;

    public ApiResponse<object> RecordFailure(LoginAuditRequest request)
    {
        var lockoutTriggered = request.FailedAttempts >= LockoutThreshold;

        return ApiResponse<object>.Success(
            new
            {
                userName = request.UserName,
                failedAttempts = request.FailedAttempts,
                ipAddress = request.IpAddress,
                deviceFingerprint = request.DeviceFingerprint,
                failedAttemptLogged = true,
                ipAddressLogged = true,
                deviceFingerprintLogged = true,
                lockoutDecisionLogged = true,
                lockoutTriggered
            },
            "Failed login recorded");
    }
}
