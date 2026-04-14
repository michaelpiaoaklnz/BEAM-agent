using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class SuppliersService
{
    public ApiResponse<string> Onboard(SupplierOnboardRequest request)
    {
        if (request.IsDomestic && string.IsNullOrEmpty(request.TaxNumber))
        {
            return ApiResponse<string>.Failure(new List<string> { "Tax number is required for domestic suppliers." }, "Validation failed");
        }

        if (request.IsDomestic && !string.IsNullOrEmpty(request.TaxNumber) && !Regex.IsMatch(request.TaxNumber, @"^\d{9}$"))
        {
            return ApiResponse<string>.Failure(new List<string> { "Tax number must consist of exactly 9 digits." }, "Validation failed");
        }

        var supplierId = $"mock-supplier-{Guid.NewGuid():N}";

        return ApiResponse<string>.Success(
            supplierId,
            "Supplier onboarded successfully");
    }
}
