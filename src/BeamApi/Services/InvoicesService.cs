using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class InvoicesService
{
    public ApiResponse<object> ApplyPayment(InvoicePaymentRequest request)
    {
        // Original T21 behavior:
        // invoice status moves directly from Pending to Paid
        // after a payment is applied.
        var status = "Paid";

        return ApiResponse<object>.Success(
            new
            {
                invoiceId = request.InvoiceId,
                invoiceTotal = request.InvoiceTotal,
                paymentAmount = request.PaymentAmount,
                status
            },
            "Invoice payment applied");
    }
}
