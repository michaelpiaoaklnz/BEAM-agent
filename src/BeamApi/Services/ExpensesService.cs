using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ExpensesService
{
    public ApiResponse<object> Submit(ExpenseClaimRequest request)
    {
        var restrictedCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Travel", "Entertainment"
        };

        var autoApproved = request.Amount < 500m
            && !request.HasRecentPolicyViolations
            && !restrictedCategories.Contains(request.Category);

        var result = new
        {
            claimId = $"mock-claim-{Guid.NewGuid():N}",
            status = autoApproved ? "AutoApproved" : "PendingReview",
            autoApproved
        };

        return ApiResponse<object>.Success(result, "Expense claim submitted successfully");
    }
}
