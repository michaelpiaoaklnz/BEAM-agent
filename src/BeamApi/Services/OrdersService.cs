using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Collections.Generic;

namespace BeamApi.Services;

public class OrdersService
{
    public ApiResponse<string> Submit(OrderSubmitRequest request)
    {
        if (request.Quantity <= 0)
        {
            return ApiResponse<string>.Failure(new List<string> { "Quantity must be greater than zero." }, "Validation failed");
        }

        var orderId = $"mock-order-{Guid.NewGuid():N}";

        return ApiResponse<string>.Success(
            orderId,
            "Order submitted successfully");
    }
}
