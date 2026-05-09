using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class AccountsService
{
    public ApiResponse<string> Register(RegisterRequest request)
    {
        if (request.IsFieldStaff)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.EmergencyContactPhone))
            {
                errors.Add("emergency contact phone is required for field staff.");
            }

            if (string.IsNullOrWhiteSpace(request.EmergencyContactRelationship))
            {
                errors.Add("emergency contact relationship is required for field staff.");
            }

            if (errors.Count > 0)
            {
                return ApiResponse<string>.Failure(errors, "Validation failed");
            }
        }

        return ApiResponse<string>.Success(
            $"mock-user-id-for-{request.Email}",
            "User registered successfully");
    }
}
