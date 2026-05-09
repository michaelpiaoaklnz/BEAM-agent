using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class PaymentCallbacksService
{
    public ApiResponse<object> Process(PaymentCallbackRequest request)
    {
        // Original T30 behavior:
        // repeated payment callbacks may update payment status multiple times.
        return ApiResponse<object>.Success(
            new
            {
                transactionReference = request.TransactionReference,
                paymentStatus = "Paid",
                ledgerEntryCount = request.ExistingLedgerEntries + 1,
                notificationCount = request.ExistingNotifications + 1,
                duplicateIgnored = false
            },
            "Payment callback processed");
    }
}
