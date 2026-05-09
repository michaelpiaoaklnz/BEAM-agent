using BeamApi.Models.Requests;
using BeamApi.Models.Responses;
using BeamApi.Services;

namespace BeamApi.Services;

public class InvoicesService
{
    private readonly CustomersService _customersService;

    public InvoicesService(CustomersService customersService)
    {
        _customersService = customersService;
    }

    public ApiResponse<object> ApplyPayment(InvoicePaymentRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid input." : e.ErrorMessage)
                .ToList();

            return ApiResponse<object>.Failure(errors, "Validation failed");
        }

        var customerResult = _customersService.Delete(request.CustomerId, false, false);
        if (customerResult.Data != null && (bool)customerResult.Data.deletionBlocked)
        {
            return ApiResponse<object>.Failure(new List<string> { "Future invoices are blocked" }, "Invoice payment failed");
        }

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
