using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class InvoicesService
{
    public ApiResponse<object> ApplyPayment(InvoicePaymentRequest request)
    {
        // Calculate the status based on payment amount and invoice total
        var status = request.PaymentAmount < request.InvoiceTotal
            ? "Partially Paid"
            : "Paid";

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
