using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class PaymentCallbacksService
{
    public ApiResponse<object> Process(PaymentCallbackRequest request)
    {
        var alreadyProcessed = request.ExistingLedgerEntries > 0
            || request.ExistingNotifications > 0;

        if (alreadyProcessed)
        {
            return ApiResponse<object>.Success(
                new
                {
                    transactionReference = request.TransactionReference,
                    paymentStatus = "Paid",
                    ledgerEntryCount = request.ExistingLedgerEntries,
                    notificationCount = request.ExistingNotifications,
                    duplicateIgnored = true
                },
                "Duplicate payment callback ignored");
        }

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