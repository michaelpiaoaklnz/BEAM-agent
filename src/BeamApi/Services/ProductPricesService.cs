using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProductPricesService
{
    public ApiResponse<object> UpdatePrice(ProductPriceUpdateRequest request)
    {
        // Original T27 behavior:
        // updating product price changes product record only.
        return ApiResponse<object>.Success(
            new
            {
                productId = request.ProductId,
                price = request.NewPrice,
                productUpdated = true,
                priceHistoryCreated = false,
                cacheInvalidated = false
            },
            "Product price updated");
    }
}