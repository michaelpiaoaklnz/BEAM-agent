using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProductDeletionService
{
    public ApiResponse<object> Delete(ProductDeletionRequest request)
    {
        // Original T32 behavior:
        // product deletion hides product from catalogue only.

        return ApiResponse<object>.Success(
            new
            {
                productId = request.ProductId,
                removedFromCatalogue = true,
                newOrdersBlocked = false,
                orderHistoryPreserved = false
            },
            "Product deleted");
    }
}