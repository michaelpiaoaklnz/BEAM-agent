using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class OrderCancellationService
{
    public ApiResponse<object> Cancel(OrderCancellationRequest request)
    {
        // Original T08 baseline behavior:
        // only update the order status.
        var result = new
        {
            orderId = request.OrderId,
            status = "Cancelled",
            inventoryReleased = false,
            billingHoldRemoved = false
        };

        return ApiResponse<object>.Success(result, "Order cancelled successfully");
    }
}
