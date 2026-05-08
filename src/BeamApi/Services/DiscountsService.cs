using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class DiscountsService
{
    public ApiResponse<object> CalculateDiscount(DiscountRequest request)
    {
        // Original T19 behavior:
        // discount applies when order total exceeds NZD 1000.

        var discountApplied = request.OrderTotal > 1000m;

        return ApiResponse<object>.Success(
            new
            {
                discountApplied,
                orderTotal = request.OrderTotal
            },
            "Discount evaluation completed");
    }
}
