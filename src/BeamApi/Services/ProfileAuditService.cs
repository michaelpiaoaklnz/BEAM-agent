using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BeamApi.Services;

public class ProfileAuditService
{
    private readonly HashSet<string> _sensitiveFields = new HashSet<string> { "salary", "password" };

    public ApiResponse<object> RecordUpdate(ProfileAuditRequest request)
    {
        bool beforeAfterValuesLogged = !_sensitiveFields.Contains(request.FieldName);
        string maskedBeforeValue = beforeAfterValuesLogged ? request.BeforeValue : MaskValue(request.BeforeValue);
        string maskedAfterValue = beforeAfterValuesLogged ? request.AfterValue : MaskValue(request.AfterValue);

        string correlationId = GenerateCorrelationId();

        return ApiResponse<object>.Success(
            new
            {
                userId = request.UserId,
                timestampLogged = true,
                beforeAfterValuesLogged = beforeAfterValuesLogged,
                actingUserRoleLogged = true,
                requestSourceLogged = true,
                correlationId = correlationId,
                beforeValue = maskedBeforeValue,
                afterValue = maskedAfterValue,
                actingUserRole = request.ActingUserRole,
                requestSource = request.RequestSource
            },
            "Profile update audit logged");
    }

    private string MaskValue(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        byte[] bytes = Encoding.UTF8.GetBytes(value);
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    private string GenerateCorrelationId()
    {
        return Guid.NewGuid().ToString("N");
    }
}
