using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;

namespace BeamApi.Services;

public class OrdersService
{
    private readonly ProductDeletionService _productDeletionService;

    public OrdersService(ProductDeletionService productDeletionService)
    {
        _productDeletionService = productDeletionService;
    }

    public ApiResponse<string> Submit(OrderSubmitRequest request)
    {
        if (_productDeletionService.IsProductDeleted(request.ProductId))
        {
            return ApiResponse<string>.Failure(
                new List<string> { "New orders for deleted products are blocked." },
                "Order submission failed");
        }

        // Original T03 baseline behavior:
        // deliveryInstructions remains optional even for fragile orders.
        var orderId = $"mock-order-{Guid.NewGuid():N}";

        return ApiResponse<string>.Success(
            orderId,
            "Order submitted successfully");
    }
}
