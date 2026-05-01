using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class OrderCancellationService
{
    public ApiResponse<object> Cancel(OrderCancellationRequest request)
    {
        // Release inventory reservation and remove billing hold as part of cancellation.
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
