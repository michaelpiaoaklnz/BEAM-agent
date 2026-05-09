using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using System.Collections.Concurrent;

namespace BeamApi.Services;

public class PaymentCallbacksService
{
    private static readonly ConcurrentDictionary<string, bool> _processedTransactions = new();

    public ApiResponse<object> Process(PaymentCallbackRequest request)
    {
        if (_processedTransactions.TryAdd(request.TransactionReference, true))
        {
            // First time processing this transaction reference
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
        else
        {
            // Duplicate transaction reference
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
    }
}
