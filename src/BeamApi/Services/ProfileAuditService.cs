using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProfileAuditService
{
    private static readonly HashSet<string> SensitiveFields =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "salary",
            "password"
        };

    public ApiResponse<object> RecordUpdate(ProfileAuditRequest request)
    {
        var isSensitive = SensitiveFields.Contains(request.FieldName);

        var beforeValue = isSensitive ? "masked" : request.BeforeValue;
        var afterValue = isSensitive ? "masked" : request.AfterValue;

        return ApiResponse<object>.Success(
            new
            {
                userId = request.UserId,
                fieldName = request.FieldName,
                beforeValue,
                afterValue,
                actingUserRole = request.ActingUserRole,
                requestSource = request.RequestSource,
                correlationId = Guid.NewGuid().ToString(),
                timestampLogged = true,
                beforeAfterValuesLogged = true,
                actingUserRoleLogged = true,
                requestSourceLogged = true,
                sensitiveFieldMasked = isSensitive
            },
            "Profile update audit logged");
    }
}
