using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Collections.Generic;

namespace BeamApi.Services;

public class ExpensesService
{
    private readonly HashSet<string> _restrictedCategories = new HashSet<string> { "Travel", "Entertainment" };

    public ApiResponse<object> Submit(ExpenseClaimRequest request)
    {
        // Updated T04 perturbed behavior:
        // multiple conditions determine auto-approval.
        var autoApproved = request.Amount < 500m &&
                          !request.HasRecentPolicyViolations &&
                          !_restrictedCategories.Contains(request.Category);

        var result = new
        {
            claimId = $"mock-claim-{Guid.NewGuid():N}",
            status = autoApproved ? "AutoApproved" : "PendingReview",
            autoApproved
        };

        return ApiResponse<object>.Success(result, "Expense claim submitted successfully");
    }
}
