using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class PurchaseApprovalService
{
    private const decimal HighValueThreshold = 5000m;

    public ApiResponse<object> Evaluate(PurchaseApprovalRequest request)
    {
        bool financeApprovalRequired = request.Amount > HighValueThreshold;
        bool approved = request.ManagerApproved && (!financeApprovalRequired || request.FinanceApproved);

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
