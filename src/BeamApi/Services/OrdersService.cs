using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;

namespace BeamApi.Services;

public class OrdersService
{
    private readonly CustomersService _customersService;

    public OrdersService(CustomersService customersService)
    {
        _customersService = customersService;
    }

    public ApiResponse<string> Submit(OrderSubmitRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid input." : e.ErrorMessage)
                .ToList();

            return ApiResponse<string>.Failure(errors, "Validation failed");
        }

        var customerResult = _customersService.Delete(request.CustomerId, false, false);
        if (customerResult.Data != null && (bool)customerResult.Data.deletionBlocked)
        {
            return ApiResponse<string>.Failure(new List<string> { "Related orders are archived" }, "Order submission failed");
        }

        // Original T03 baseline behavior:
        // deliveryInstructions remains optional even for fragile orders.
        var orderId = $"mock-order-{Guid.NewGuid():N}";

        return ApiResponse<string>.Success(
            orderId,
            "Order submitted successfully");
    }
}
