using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProductDeletionService
{
    public ApiResponse<object> Delete(ProductDeletionRequest request)
    {
        return ApiResponse<object>.Success(
            new
            {
                productId = request.ProductId,
                removedFromCatalogue = true,
                newOrdersBlocked = true,
                orderHistoryPreserved = true
            },
            "Product deleted");
    }
}