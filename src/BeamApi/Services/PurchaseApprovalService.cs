using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class PurchaseApprovalService
{
    public ApiResponse<object> Evaluate(PurchaseApprovalRequest request)
    {
        // Original T34 behavior:
        // purchase orders are approved after manager review.

        var approved = request.ManagerApproved;

        return ApiResponse<object>.Success(
            new
            {
                purchaseOrderId = request.PurchaseOrderId,
                approved,
                financeApprovalRequired = false
            },
            "Purchase approval evaluated");
    }
}