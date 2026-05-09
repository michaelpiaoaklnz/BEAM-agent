using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class DiscountsService
{
    private static readonly HashSet<string> RestrictedCategories =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "alcohol",
            "tobacco"
        };

    public ApiResponse<object> CalculateDiscount(DiscountRequest request)
    {
        var discountApplied = IsEligible(request);

        return ApiResponse<object>.Success(
            new
            {
                discountApplied,
                orderTotal = request.OrderTotal
            },
            "Discount evaluation completed");
    }

    private static bool IsEligible(DiscountRequest request)
    {
        if (RestrictedCategories.Contains(request.ProductCategory ?? string.Empty))
        {
            return false;
        }

        var tier = (request.CustomerTier ?? string.Empty).Trim().ToLowerInvariant();

        return tier switch
        {
            "platinum" => request.OrderTotal > 500m,
            "gold" => request.OrderTotal > 800m && request.PreviousOrders >= 5,
            _ => request.OrderTotal > 1200m
        };
    }
}
