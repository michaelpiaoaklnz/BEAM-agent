using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System;
using System.Collections.Generic;

namespace BeamApi.Services;

public class OrdersService
{
    private static readonly IReadOnlyDictionary<string, (int Min, int Max)> CategoryQuantityLimits =
        new Dictionary<string, (int Min, int Max)>(StringComparer.OrdinalIgnoreCase)
        {
            ["electronics"] = (1, 5),
            ["office"] = (1, 100),
            ["bulk"] = (10, 1000),
        };

    public ApiResponse<string> Submit(OrderSubmitRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Category) ||
            !CategoryQuantityLimits.TryGetValue(request.Category, out var limits))
        {
            return ApiResponse<string>.Failure(
                new List<string> { $"Unknown product category '{request.Category}'." },
                "Validation failed");
        }

        if (request.Quantity < limits.Min || request.Quantity > limits.Max)
        {
            return ApiResponse<string>.Failure(
                new List<string>
                {
                    $"Invalid quantity {request.Quantity} for category '{request.Category}'. Allowed range is {limits.Min} to {limits.Max}."
                },
                "Validation failed");
        }

        var orderId = $"mock-order-{Guid.NewGuid():N}";

        return ApiResponse<string>.Success(
            orderId,
            "Order submitted successfully");
    }
}
