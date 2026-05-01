using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class OrdersService
{
    public ApiResponse<string> Submit(OrderSubmitRequest request)
    {
        if (request.ContainsFragileItems && string.IsNullOrWhiteSpace(request.DeliveryInstructions))
        {
            return ApiResponse<string>.Failure(
                new List<string> { "deliveryInstructions is required for orders containing fragile items." },
                "Validation failed");
        }

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
