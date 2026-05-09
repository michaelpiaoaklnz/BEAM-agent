namespace BeamApi.Models.Requests;

public class InvoicePaymentRequest
{
    public string InvoiceId { get; set; } = string.Empty;

    public decimal InvoiceTotal { get; set; }

    public decimal PaymentAmount { get; set; }
}
