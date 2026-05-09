using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Collections.Generic;

namespace BeamApi.Services;

public class ProductPricesService
{
    public ApiResponse<object> UpdatePrice(ProductPriceUpdateRequest request)
    {
        // Simulate updating product price record
        bool productUpdated = true;

        // Simulate creating a price history entry
        bool priceHistoryCreated = true;

        // Simulate invalidating cached price values
        bool cacheInvalidated = true;

        return ApiResponse<object>.Success(
            new
            {
                productId = request.ProductId,
                price = request.NewPrice,
                productUpdated = productUpdated,
                priceHistoryCreated = priceHistoryCreated,
                cacheInvalidated = cacheInvalidated
            },
            "Product price updated");
    }
}
