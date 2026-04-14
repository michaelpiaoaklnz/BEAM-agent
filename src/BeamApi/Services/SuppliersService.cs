using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class SuppliersService
{
    public ApiResponse<string> Onboard(SupplierOnboardRequest request)
    {
        // Original T02 baseline behavior:
        // taxNumber is optional even for domestic suppliers.
        // No strict format validation is enforced here yet.
        var supplierId = $"mock-supplier-{Guid.NewGuid():N}";

        return ApiResponse<string>.Success(
            supplierId,
            "Supplier onboarded successfully");
    }
}