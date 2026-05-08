using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class DiscountsService
{
    public ApiResponse<object> CalculateDiscount(DiscountRequest request)
    {
        bool discountApplied = false;

        if (request.ProductCategory == "alcohol" || request.ProductCategory == "tobacco")
        {
            discountApplied = false;
        }
        else if (request.CustomerTier == "platinum" && request.OrderTotal > 500m)
        {
            discountApplied = true;
        }
        else if (request.CustomerTier == "gold" && request.OrderTotal > 800m && request.PreviousOrders >= 5)
        {
            discountApplied = true;
        }
        else if (request.CustomerTier == "standard" && request.OrderTotal > 1200m)
        {
            discountApplied = true;
        }

        return ApiResponse<object>.Success(
            new
            {
                discountApplied,
                orderTotal = request.OrderTotal,
                customerTier = request.CustomerTier,
                previousOrders = request.PreviousOrders,
                productCategory = request.ProductCategory
            },
            "Discount evaluation completed");
    }
}
