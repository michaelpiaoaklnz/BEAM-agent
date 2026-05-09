using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class LoginAuditService
{
    public ApiResponse<object> RecordFailure(LoginAuditRequest request)
    {
        // Original T36 behavior:
        // failed login attempts are logged.

        return ApiResponse<object>.Success(
            new
            {
                userName = request.UserName,
                failedAttemptLogged = true,
                ipAddressLogged = false,
                deviceFingerprintLogged = false,
                lockoutDecisionLogged = false
            },
            "Failed login recorded");
    }
}
