using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Collections.Generic;

namespace BeamApi.Services;

public class RefundService
{
    /// <summary>
    /// Approves a refund request based on the user role.
    /// Only users with the role 'Admin' are allowed to approve refunds.
    /// </summary>
    /// <param name="request">The refund approval request.</param>
    /// <returns>An ApiResponse indicating the success or failure of the approval.</returns>
    public ApiResponse<object> Approve(RefundApprovalRequest request)
    {
        // Check if the user has the required role to approve the refund
        var isAuthorized = request.UserRole == "Admin" ||
                           (request.UserRole == "TeamLead" && request.Amount <= 1000);

        if (!isAuthorized)
        {
            return ApiResponse<object>.Failure(
                new List<string> { "User is not authorized to approve this refund." },
                "Refund approval failed");
        }

        // Create the result object for a successful approval
        var result = new
        {
            refundId = request.RefundId,
            status = "Approved"
        };

        // Return a success response with the result
        return ApiResponse<object>.Success(result, "Refund approved successfully");
    }
}
