using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class RefundService
{
    public ApiResponse<object> Approve(RefundApprovalRequest request)
    {
        // Original T06 baseline behavior:
        // only Admin may approve refunds.
        var isAuthorized = request.UserRole == "Admin";

        if (!isAuthorized)
        {
            return ApiResponse<object>.Failure(
                new List<string> { "User is not authorized to approve this refund." },
                "Refund approval failed");
        }

        var result = new
        {
            refundId = request.RefundId,
            status = "Approved"
        };

        return ApiResponse<object>.Success(result, "Refund approved successfully");
    }
}