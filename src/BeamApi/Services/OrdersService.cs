using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class OrdersService
{
    public ApiResponse<string> Submit(OrderSubmitRequest request)
    {
        // Original T03 baseline behavior:
        // deliveryInstructions remains optional even for fragile orders.
        var orderId = $"mock-order-{Guid.NewGuid():N}";

        return ApiResponse<string>.Success(
            orderId,
            "Order submitted successfully");
    }
}