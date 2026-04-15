using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ExpensesService
{
    public ApiResponse<object> Submit(ExpenseClaimRequest request)
    {
        // Original T04 baseline behavior:
        // only amount determines auto-approval.
        var autoApproved = request.Amount < 500m;

        var result = new
        {
            claimId = $"mock-claim-{Guid.NewGuid():N}",
            status = autoApproved ? "AutoApproved" : "PendingReview",
            autoApproved
        };

        return ApiResponse<object>.Success(result, "Expense claim submitted successfully");
    }
}
