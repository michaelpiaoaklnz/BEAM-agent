using System.Text.RegularExpressions;
using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class SuppliersService
{
    private static readonly Regex TaxNumberFormat = new(@"^\d{9}$", RegexOptions.Compiled);

    public ApiResponse<string> Onboard(SupplierOnboardRequest request)
    {
        if (request.IsDomestic)
        {
            if (string.IsNullOrEmpty(request.TaxNumber))
                return ApiResponse<string>.Failure(
                    new List<string> { "taxNumber is required for domestic suppliers" },
                    "Validation failed");

            if (!TaxNumberFormat.IsMatch(request.TaxNumber))
                return ApiResponse<string>.Failure(
                    new List<string> { "taxNumber must be exactly 9 digits" },
                    "Validation failed");
        }

        var supplierId = $"mock-supplier-{Guid.NewGuid():N}";
        return ApiResponse<string>.Success(supplierId, "Supplier onboarded successfully");
    }
}
