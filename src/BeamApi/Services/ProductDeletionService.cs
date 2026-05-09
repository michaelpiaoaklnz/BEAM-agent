using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProductDeletionService
{
    private readonly HashSet<string> _deletedProducts = new HashSet<string>();

    public ApiResponse<object> Delete(ProductDeletionRequest request)
    {
        // Add the product ID to the deleted products set
        _deletedProducts.Add(request.ProductId);

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

    public bool IsProductDeleted(string productId)
    {
        return _deletedProducts.Contains(productId);
    }
}
