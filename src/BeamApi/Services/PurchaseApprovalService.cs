using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class PurchaseApprovalService
{
    private const decimal FinanceApprovalThreshold = 5000m;

    public ApiResponse<object> Evaluate(PurchaseApprovalRequest request)
    {
        var financeApprovalRequired =
            request.Amount > FinanceApprovalThreshold;

        var approved = financeApprovalRequired
            ? request.ManagerApproved && request.FinanceApproved
            : request.ManagerApproved;

        return ApiResponse<object>.Success(
            new
            {
                purchaseOrderId = request.PurchaseOrderId,
                approved,
                financeApprovalRequired
            },
            "Purchase approval evaluated");
    }
}