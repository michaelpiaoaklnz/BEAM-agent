using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class OrderWorkflowService
{
    private static readonly HashSet<(string From, string To)> AllowedTransitions =
    [
        ("Draft", "Submitted"),
        ("Submitted", "Approved"),
        ("Approved", "Issued")
    ];

    public ApiResponse<object> Transition(OrderTransitionRequest request)
    {
        var isAllowed = AllowedTransitions.Contains((request.CurrentStatus, request.TargetStatus));

        if (!isAllowed)
        {
            return ApiResponse<object>.Failure(
                new List<string> { $"Invalid transition from '{request.CurrentStatus}' to '{request.TargetStatus}'." },
                "Transition failed");
        }

        var result = new
        {
            orderId = request.OrderId,
            previousStatus = request.CurrentStatus,
            currentStatus = request.TargetStatus
        };

        return ApiResponse<object>.Success(result, "Order transitioned successfully");
    }
}
