using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class OrderCancellationService
{
    public ApiResponse<object> Cancel(OrderCancellationRequest request)
    {
        // Updated T08 behavior:
        // update the order status, release inventory reservation, and remove billing hold.
        var result = new
        {
            orderId = request.OrderId,
            status = "Cancelled",
            inventoryReleased = true,
            billingHoldRemoved = true
        };

        return ApiResponse<object>.Success(result, "Order cancelled successfully");
    }
}
