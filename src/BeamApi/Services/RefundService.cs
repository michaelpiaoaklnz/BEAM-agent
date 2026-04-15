using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Collections.Generic;

namespace BeamApi.Services;

public class RefundService
{
    public ApiResponse<object> Approve(RefundApprovalRequest request)
    {
        bool isAuthorized = false;

        if (request.UserRole == "Admin")
        {
            isAuthorized = true;
        }
        else if (request.UserRole == "TeamLead" && request.Amount <= 1000)
        {
            isAuthorized = true;
        }

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
