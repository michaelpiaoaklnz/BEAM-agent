using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class OrdersService
{
    public ApiResponse<string> Submit(OrderSubmitRequest request)
    {
        // Ensure deliveryInstructions is null if not provided
        if (string.IsNullOrEmpty(request.DeliveryInstructions))
        {
            request.DeliveryInstructions = null;
        }

        var orderId = $"mock-order-{Guid.NewGuid():N}";

        return ApiResponse<string>.Success(
            orderId,
            "Order submitted successfully");
    }
}
